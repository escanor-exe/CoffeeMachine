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

            UserManager userManager = new(userRepository);
            AuthenticationService authenticationService = new(userManager);

            LoginView loginView = new LoginView(authenticationService, userManager);
            RegisterView registerView = new RegisterView(userManager);
            StartupView startupView = new(loginView, registerView);

            startupView.Launch();
        }
    }
}
