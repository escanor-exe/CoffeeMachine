using System.Security.Cryptography;
using System.Text;

namespace CoffeeMachine.Helpers
{
    public static class HashHelper
    {
        private static readonly string Salt = "Some4Strong9Salt2For*Coffee&Machine$Application";

        public static string GetPasswordHash(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + Salt);
            byte[] hashData = SHA256.HashData(bytes);

            return Convert.ToBase64String(hashData);
        }
    }
}
