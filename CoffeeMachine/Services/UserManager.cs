using CoffeeMachine.Models;
using CoffeeMachine.Repository;
using static CoffeeMachine.Helpers.HashHelper;

namespace CoffeeMachine.Services
{
    public class UserManager
    {
        private readonly UserRepository _repository;

        public UserManager(UserRepository repository)
        {
            _repository = repository;
        }

        public void Add(User user)
        {
            user.PasswordHash = GetPasswordHash(user.PasswordHash);
            _repository.Add(user);
        }

        public User? Get(string username)
        {
            return _repository.GetUser(username);
        }

        public bool Contains(string username)
        {
            return _repository.Contains(username);
        }

        public bool VerifyCredentials(string username, string passwordHash)
        {
            return _repository.VerifyCredentials(username, passwordHash);
        }
    }
}
