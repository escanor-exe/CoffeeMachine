using CoffeeMachine.Models.Enums;

namespace CoffeeMachine.Models
{
    public class OrderItem
    {
        public CoffeeType CoffeeType { get; }

        public IReadOnlyList<IngredientRequirement> Recipe { get; }

        public OrderItem(
            CoffeeType coffeeType,
            IReadOnlyList<IngredientRequirement> recipe)
        {
            CoffeeType = coffeeType;
            Recipe = recipe;
        }
    }
}
