using PoeLeagueTracker.Shared.Enums;

namespace PoeLeagueTracker.Domain.Users
{
    public class UserFactory
    {
        private readonly IPasswordHasher _passwordHasher;

        public UserFactory(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public User Create(string username, string password, UserRole role)
        {
            if (UserGuardClauses(username, password))
            {
                var hashedPassword = _passwordHasher.HashPassword(password);

                return User.CreateUser(username, hashedPassword, role);
            }
            else
            {
                throw new Exception("Username or password did not adhere to requirements.");
            }
        }

        private bool UserGuardClauses(string username, string password)
        {
            if (username == null || username == "" || username.Length > 20) return false;
            if (password == null || password == "" || password.Length < 8) return false;
            return true;
        }
    }
}
