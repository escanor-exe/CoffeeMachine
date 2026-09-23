namespace CoffeeMachine.Models
{
    public class Machine
    {
        public int MachineId { get; }

        public bool IsAvailable { get; private set; }

        public OrderItem? CurrentOrder { get; private set; }

        public Machine(int machineId)
        {
            MachineId = machineId;
            IsAvailable = true;
        }

        public void AssignOrder(OrderItem order)
        {
            CurrentOrder = order;
            IsAvailable = false;
        }

        public void CompleteOrder()
        {
            CurrentOrder = null;
            IsAvailable = true;
        }
    }
}
