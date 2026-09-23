namespace CoffeeMachine.Views
{
    public class StartupView
    {
        private readonly LoginView _loginView;
        private readonly RegisterView _registerView;
        private readonly CoffeeView _coffeeView;

        public StartupView(LoginView loginView, RegisterView registerView, CoffeeView coffeeView)
        {
            _loginView = loginView;
            _registerView = registerView;
            _coffeeView = coffeeView;
        }

        public async Task LaunchAsync()
        {
            bool result = false;

            while (true)
            {
                Console.WriteLine(@"[1] Login
[2] Register
[3] Exit

Enter your choice: ");

                ConsoleKey userChoice = Console.ReadKey(true).Key;

                if (userChoice is ConsoleKey.D3 or ConsoleKey.NumPad3)
                {
                    Console.WriteLine("Press any key to exit the application...");
                    Console.ReadKey();
                    return;
                }

                switch (userChoice)
                {
                    case ConsoleKey.D1 or ConsoleKey.NumPad1:
                        result = _loginView.Login();
                        if (result)
                        {
                            Console.WriteLine("Login successful...");

                            await _coffeeView.RunAsync(CancellationToken.None);

                            return;
                        }
                        else
                        {
                            Console.WriteLine("Login failed...");
                        }
                        break;

                    case ConsoleKey.D2 or ConsoleKey.NumPad2:
                        result = _registerView.Register();
                        if (result)
                        {
                            Console.WriteLine("Registration successful...");
                        }
                        else
                        {
                            Console.WriteLine("Registration failed...");
                        }
                        break;

                    default:
                        Console.WriteLine("Your choice is invalid, try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
