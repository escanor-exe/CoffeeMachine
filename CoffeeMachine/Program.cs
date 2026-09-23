using CoffeeMachine.Models;
using CoffeeMachine.Models.Enums;
using CoffeeMachine.Repository;
using CoffeeMachine.Services;
using CoffeeMachine.Views;

namespace CoffeeMachine
{
    internal class Program
    {
        private static async Task Main()
        {
            UserRepository userRepository = new("users.json");
            InventoryRepository inventoryRepository = new InventoryRepository(
            [
            new InventoryItem(Ingredient.CoffeeBeans, 100),
            new InventoryItem(Ingredient.Water, 500),
            new InventoryItem(Ingredient.Milk, 300),
            new InventoryItem(Ingredient.Sugar, 100)
            ]);

            UserManager userManager = new(userRepository);
            AuthenticationService authenticationService = new(userManager);

            Notifier notifier = new();

            List<Machine> machines =
            [
                new Machine(1),
                new Machine(2),
                new Machine(3)
            ];

            CoffeeService coffeeService = new(
                inventoryRepository,
                machines,
                notifier,
                sourcingTime: TimeSpan.FromSeconds(2),
                preparationTime: TimeSpan.FromSeconds(5),
                restockInterval: TimeSpan.FromSeconds(10));

            LoginView loginView = new LoginView(authenticationService, userManager);
            RegisterView registerView = new RegisterView(userManager);

            CoffeeView coffeeView = new(coffeeService, notifier);

            StartupView startupView = new(loginView, registerView, coffeeView);

            await startupView.LaunchAsync();
        }
    }
}
