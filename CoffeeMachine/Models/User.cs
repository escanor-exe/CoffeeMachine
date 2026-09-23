namespace CoffeeMachine.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string name, string username, string passwordHash)
        {
            Name = name;
            Username = username;
            PasswordHash = passwordHash;
        }

        public User(User other)
        {
            this.Id = other.Id;
            this.Name = other.Name;
            this.Username = other.Username;
            this.PasswordHash = string.Empty;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}
