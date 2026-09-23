using CoffeeMachine.Models;

namespace CoffeeMachine.Application
{
    public static class UserSession
    {
        public static User? CurrentUser { get; set; }

        public static bool IsLoggedIn => CurrentUser is not null;

        public static void Reset() => CurrentUser = null;
    }
}
