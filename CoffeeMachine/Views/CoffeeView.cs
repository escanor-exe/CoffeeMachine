using CoffeeMachine.Models;
using CoffeeMachine.Models.Enums;
using CoffeeMachine.Services;

namespace CoffeeMachine.Views
{
    public class CoffeeView
    {
        private readonly CoffeeService _coffeeService;
        private readonly Notifier _notifier;

        public CoffeeView(
            CoffeeService coffeeService,
            Notifier notifier)
        {
            _coffeeService = coffeeService;
            _notifier = notifier;

            _notifier.OrderStatusChanged += OnOrderStatusChanged;
            _notifier.BrewingStatusChanged += OnBrewingStatusChanged;
        }

        public async Task RunAsync(CancellationToken cancellationToken)
        {
            _coffeeService.Start(cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine();
                Console.WriteLine("===== Coffee Machine =====");
                Console.WriteLine("1. Espresso");
                Console.WriteLine("2. Americano");
                Console.WriteLine("3. Cappuccino");
                Console.WriteLine("4. Latte");
                Console.WriteLine("Q. Quit");
                Console.Write("Select coffee: ");

                string? input = Console.ReadLine();

                if (string.Equals(input, "Q", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                if (!TryGetCoffeeType(input, out CoffeeType coffeeType))
                {
                    Console.WriteLine("Invalid selection.");
                    continue;
                }

                OrderItem order = CreateOrder(coffeeType);

                _coffeeService.PlaceOrder(order);

                Console.WriteLine(
                    $"{coffeeType} order placed.");
            }

            await Task.CompletedTask;
        }

        private static bool TryGetCoffeeType(
            string? input,
            out CoffeeType coffeeType)
        {
            coffeeType = default;

            return input switch
            {
                "1" => SetCoffeeType(
                    CoffeeType.Espresso,
                    out coffeeType),

                "2" => SetCoffeeType(
                    CoffeeType.Americano,
                    out coffeeType),

                "3" => SetCoffeeType(
                    CoffeeType.Cappuccino,
                    out coffeeType),

                "4" => SetCoffeeType(
                    CoffeeType.Latte,
                    out coffeeType),

                _ => false
            };
        }

        private static bool SetCoffeeType(
            CoffeeType type,
            out CoffeeType coffeeType)
        {
            coffeeType = type;
            return true;
        }

        private static OrderItem CreateOrder(
            CoffeeType coffeeType)
        {
            IReadOnlyList<IngredientRequirement> recipe =
                coffeeType switch
                {
                    CoffeeType.Espresso =>
                    [
                        new(Ingredient.CoffeeBeans, 20),
                    new(Ingredient.Water, 30)
                    ],

                    CoffeeType.Americano =>
                    [
                        new(Ingredient.CoffeeBeans, 20),
                    new(Ingredient.Water, 100)
                    ],

                    CoffeeType.Cappuccino =>
                    [
                        new(Ingredient.CoffeeBeans, 20),
                    new(Ingredient.Water, 30),
                    new(Ingredient.Milk, 100)
                    ],

                    CoffeeType.Latte =>
                    [
                        new(Ingredient.CoffeeBeans, 20),
                    new(Ingredient.Water, 30),
                    new(Ingredient.Milk, 150)
                    ],

                    _ => throw new ArgumentOutOfRangeException(
                        nameof(coffeeType))
                };

            return new OrderItem(
                coffeeType,
                recipe);
        }

        private static void OnOrderStatusChanged(
            OrderItem order,
            OrderStatus status)
        {
            Console.WriteLine(
                $"[Order] {order.CoffeeType}: {status}");
        }

        private static void OnBrewingStatusChanged(
            OrderItem order,
            BrewingStatus status)
        {
            Console.WriteLine(
                $"[Brewing] {order.CoffeeType}: {status}");
        }
    }
}
