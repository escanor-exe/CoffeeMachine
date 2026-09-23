using CoffeeMachine.Models;

namespace CoffeeMachine.Repository.Interfaces
{
    public interface IInventoryRepository
    {
        bool TryConsume(IReadOnlyList<IngredientRequirement> requirements);

        void Restock();
    }
}
