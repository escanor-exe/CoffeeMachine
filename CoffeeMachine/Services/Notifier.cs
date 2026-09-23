using CoffeeMachine.Models;
using CoffeeMachine.Models.Enums;

namespace CoffeeMachine.Services
{
    public class Notifier
    {
        public event Action<OrderItem, OrderStatus>? OrderStatusChanged;

        public event Action<OrderItem, BrewingStatus>? BrewingStatusChanged;

        public void NotifyOrderStatus(
            OrderItem order,
            OrderStatus status)
        {
            OrderStatusChanged?.Invoke(order, status);
        }

        public void NotifyBrewingStatus(
            OrderItem order,
            BrewingStatus status)
        {
            BrewingStatusChanged?.Invoke(order, status);
        }
    }
}
