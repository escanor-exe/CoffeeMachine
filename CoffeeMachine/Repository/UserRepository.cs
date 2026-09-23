using CoffeeMachine.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CoffeeMachine.Repository
{
    public class UserRepository
    {
        private readonly List<User> _users;
        private JsonSerializerOptions _options;
        private readonly string _filePath;

        public UserRepository(string filePath)
        {
            _options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(),
                }
            };

            _filePath = filePath;
            Initialize();

            _users = LoadUsers();
        }

        public void Add(User user)
        {
            _users.Add(user);
            WriteToFile();

        }

        public User? GetUser(string username)
        {
            User? user = _users.FirstOrDefault(user => user.Username == username);
            return user is not null ? new User(user) : null;
        }

        public bool Contains(string username)
        {
            return _users.Any(user => user.Username.Equals(username));
        }

        public bool VerifyCredentials(string username, string passwordHash)
        {
            return _users.Any(user => user.Username.Equals(username) && user.PasswordHash.Equals(passwordHash));
        }

        private void Initialize()
        {
            if (File.Exists(_filePath))
            {
                return;
            }

            File.WriteAllText(_filePath, $"[{Environment.NewLine}]");
        }

        private List<User> LoadUsers()
        {
            string jsonContent = File.ReadAllText(_filePath);
            List<User>? users = JsonSerializer.Deserialize<List<User>>(jsonContent, _options);

            return users is null ? new() : users;
        }

        private void WriteToFile()
        {
            string jsonData = JsonSerializer.Serialize(_users);
            File.WriteAllText(_filePath, jsonData);
        }
    }
}
