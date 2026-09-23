using CoffeeMachine.Application;
using CoffeeMachine.Services;
using static CoffeeMachine.Helpers.ConsoleHelper;

namespace CoffeeMachine.Views
{
    public class LoginView
    {
        private readonly AuthenticationService _authService;
        private readonly UserManager _userManager;

        public LoginView(AuthenticationService authService, UserManager userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        public bool Login()
        {
            string username = GetUserName("Enter your username: ");
            string password = GetPassword("Enter your password: ");

            bool authStatus = _authService.VerifyCredentials(username, password);
            UserSession.CurrentUser = authStatus ? _userManager.Get(username) : null;

            return authStatus;
        }
    }
}
