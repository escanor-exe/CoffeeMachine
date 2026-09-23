using CoffeeMachine.Models;
using CoffeeMachine.Models.Enums;
using CoffeeMachine.Repository;
using CoffeeMachine.Services;
using CoffeeMachine.Views;

namespace CoffeeMachine
{
    internal class Program
    {
        private static void Main()
        {
            UserRepository userRepository = new("users.json");
            InventoryRepository inventory = new InventoryRepository(
            [
            new InventoryItem(Ingredient.CoffeeBeans, 100),
            new InventoryItem(Ingredient.Water, 500),
            new InventoryItem(Ingredient.Milk, 300),
            new InventoryItem(Ingredient.Sugar, 100)
            ]);

            UserManager userManager = new(userRepository);
            AuthenticationService authenticationService = new(userManager);

            LoginView loginView = new LoginView(authenticationService, userManager);
            RegisterView registerView = new RegisterView(userManager);
            StartupView startupView = new(loginView, registerView);

            startupView.Launch();
        }
    }
}
