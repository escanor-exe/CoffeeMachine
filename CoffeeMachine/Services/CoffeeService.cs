using CoffeeMachine.Models;
using CoffeeMachine.Models.Enums;
using CoffeeMachine.Repository.Interfaces;
using System.Collections.Concurrent;

namespace CoffeeMachine.Services
{
    public class CoffeeService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly List<Machine> _machines;
        private readonly Notifier _notifier;

        private readonly ConcurrentQueue<OrderItem> _orderQueue = new();
        private readonly SemaphoreSlim _orderSignal = new(0);

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private readonly List<Task> _machineTasks = new();

        private readonly TimeSpan _sourcingTime;
        private readonly TimeSpan _preparationTime;
        private readonly TimeSpan _restockInterval;

        public CoffeeService(
            IInventoryRepository inventoryRepository,
            IEnumerable<Machine> machines,
            Notifier notifier,
            TimeSpan sourcingTime,
            TimeSpan preparationTime,
            TimeSpan restockInterval)
        {
            _inventoryRepository = inventoryRepository;
            _machines = machines.ToList();
            _notifier = notifier;

            _sourcingTime = sourcingTime;
            _preparationTime = preparationTime;
            _restockInterval = restockInterval;

            StartMachineWorkers();
            StartRestocking();
        }

        public void Start(CancellationToken cancellationToken)
        {
            // Start machine workers
            foreach (Machine machine in _machines)
            {
                _ = ProcessMachineAsync(machine, cancellationToken);
            }

            // Start periodic restocking
            _ = RestockPeriodicallyAsync(cancellationToken);
        }

        public void PlaceOrder(OrderItem order)
        {
            _orderQueue.Enqueue(order);

            _notifier.NotifyOrderStatus(
                order,
                OrderStatus.Queued);

            _orderSignal.Release();
        }

        private void StartMachineWorkers()
        {
            foreach (var machine in _machines)
            {
                Task worker = ProcessMachineAsync(
                    machine,
                    _cancellationTokenSource.Token);

                _machineTasks.Add(worker);
            }
        }

        private async Task ProcessMachineAsync(
    Machine machine,
    CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await _orderSignal.WaitAsync(cancellationToken);

                    if (!_orderQueue.TryDequeue(out OrderItem? order))
                    {
                        continue;
                    }

                    machine.AssignOrder(order);

                    try
                    {
                        await ProcessOrderAsync(
                            machine,
                            order,
                            cancellationToken);
                    }
                    finally
                    {
                        machine.CompleteOrder();
                    }
                }
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                // Expected during application shutdown.
            }
        }

        private async Task ProcessOrderAsync(
    Machine machine,
    OrderItem order,
    CancellationToken cancellationToken)
        {
            _notifier.NotifyOrderStatus(
                order,
                OrderStatus.Processing);

            bool stockAvailable =
                _inventoryRepository.TryConsume(order.Recipe);

            if (!stockAvailable)
            {
                _notifier.NotifyOrderStatus(
                    order,
                    OrderStatus.Failed);

                return;
            }

            try
            {
                _notifier.NotifyBrewingStatus(
                    order,
                    BrewingStatus.Started);

                await SourceIngredientsAsync(
                    order,
                    cancellationToken);

                await PrepareCoffeeAsync(
                    order,
                    cancellationToken);

                _notifier.NotifyBrewingStatus(
                    order,
                    BrewingStatus.Completed);

                _notifier.NotifyOrderStatus(
                    order,
                    OrderStatus.Completed);
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                _notifier.NotifyOrderStatus(
                    order,
                    OrderStatus.Cancelled);

                throw;
            }
        }

        private async Task SourceIngredientsAsync(
            OrderItem order,
            CancellationToken cancellationToken)
        {
            _notifier.NotifyBrewingStatus(
                order,
                BrewingStatus.Sourcing);

            await Task.Delay(
                _sourcingTime,
                cancellationToken);
        }

        private async Task PrepareCoffeeAsync(
            OrderItem order,
            CancellationToken cancellationToken)
        {
            _notifier.NotifyBrewingStatus(
                order,
                BrewingStatus.Preparing);

            await Task.Delay(
                _preparationTime,
                cancellationToken);
        }

        private void StartRestocking()
        {
            _ = RestockPeriodicallyAsync(
                _cancellationTokenSource.Token);
        }

        private async Task RestockPeriodicallyAsync(
            CancellationToken cancellationToken)
        {
            try
            {
                while (true)
                {
                    await Task.Delay(
                        _restockInterval,
                        cancellationToken);

                    _inventoryRepository.Restock();
                }
            }
            catch (OperationCanceledException)
                when (cancellationToken.IsCancellationRequested)
            {
                // Expected during shutdown.
            }
        }
    }
}