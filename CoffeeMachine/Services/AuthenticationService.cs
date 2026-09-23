using static CoffeeMachine.Helpers.HashHelper;

namespace CoffeeMachine.Services
{
    public class AuthenticationService
    {
        private readonly UserManager _manager;

        public AuthenticationService(UserManager manager)
        {
            _manager = manager;
        }

        public bool VerifyCredentials(string username, string password)
        {
            return _manager.VerifyCredentials(username, GetPasswordHash(password));
        }
    }
}
