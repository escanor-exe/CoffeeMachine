namespace CoffeeMachine.Helpers
{
    public static class ConsoleHelper
    {
        public static string GetName(string prompt)
        {
            Console.Write(prompt);
            string name = Console.ReadLine();

            // validate before sending
            return name;
        }

        public static string GetUserName(string prompt)
        {
            Console.Write(prompt);
            string userName = Console.ReadLine();

            // validate before sending
            return userName;
        }

        public static string GetPassword(string prompt)
        {
            Console.Write(prompt);
            string password = Console.ReadLine();

            return password;
        }
    }
}
