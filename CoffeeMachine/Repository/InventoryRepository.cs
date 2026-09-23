using CoffeeMachine.Models;
using CoffeeMachine.Models.Enums;
using CoffeeMachine.Repository.Interfaces;

namespace CoffeeMachine.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly Dictionary<Ingredient, InventoryItem> _inventory;

        private readonly object _lock = new();

        public InventoryRepository(
            IEnumerable<InventoryItem> inventoryItems)
        {
            _inventory = inventoryItems.ToDictionary(
                item => item.Ingredient);
        }

        public bool TryConsume(
            IReadOnlyList<IngredientRequirement> requirements)
        {
            lock (_lock)
            {
                foreach (IngredientRequirement requirement in requirements)
                {
                    if (!_inventory.TryGetValue(
                            requirement.Ingredient,
                            out InventoryItem? inventoryItem))
                    {
                        return false;
                    }

                    if (inventoryItem.CurrentStock < requirement.Quantity)
                    {
                        return false;
                    }
                }

                foreach (IngredientRequirement requirement in requirements)
                {
                    InventoryItem inventoryItem =
                        _inventory[requirement.Ingredient];

                    inventoryItem.Consume(requirement.Quantity);
                }

                return true;
            }
        }

        public void Restock()
        {
            lock (_lock)
            {
                foreach (InventoryItem inventoryItem in _inventory.Values)
                {
                    inventoryItem.Restock();
                }
            }
        }
    }
}
