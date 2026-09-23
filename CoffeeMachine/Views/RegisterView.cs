using CoffeeMachine.Models;
using CoffeeMachine.Services;
using static CoffeeMachine.Helpers.ConsoleHelper;

namespace CoffeeMachine.Views
{
    public class RegisterView
    {
        private readonly UserManager _manager;

        public RegisterView(UserManager manager)
        {
            _manager = manager;
        }

        public bool Register()
        {
            string name = GetName("Enter your name: ");
            string username = GetUserName("Enter your username: ");
            if (_manager.Contains(username))
            {
                return false;
            }

            string password = GetPassword("Enter your password: ");
            User user = new(name, username, password);
            _manager.Add(user);

            return true;
        }
    }
}
