using CoffeeMachine.Models.Enums;

namespace CoffeeMachine.Models
{
    public class InventoryItem
    {
        public Ingredient Ingredient { get; }

        public int CurrentStock { get; private set; }

        public int MaxStock { get; }

        public InventoryItem(Ingredient ingredient, int maxStock)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(maxStock);

            Ingredient = ingredient;
            MaxStock = maxStock;
            CurrentStock = 0;
        }

        public void Consume(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity));
            }

            if (quantity > CurrentStock)
            {
                throw new InvalidOperationException(
                    $"Insufficient stock for {Ingredient}.");
            }

            CurrentStock -= quantity;
        }

        public void Restock()
        {
            CurrentStock = MaxStock;
        }
    }
}
