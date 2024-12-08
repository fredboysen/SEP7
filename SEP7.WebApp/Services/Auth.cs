using System.Collections.Generic;
using System.Threading.Tasks;
using SEP7.WebApp.Models;

namespace SEP7.WebApp.Services
{
    public class AuthService : IAuthService
    {
        // Predefined list of users
        private static readonly List<User> Users = new List<User>
        {
            new User { Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Username = "user", Password = "user123", Role = "User" }
        };

        public Task<User> LoginAsync(User user)
        {
            // Simulate a login by validating against the predefined users
            var foundUser = Users.Find(u =>
                u.Username.Equals(user.Username, System.StringComparison.OrdinalIgnoreCase) &&
                u.Password == user.Password);

            return Task.FromResult(foundUser); // Return user or null
        }
    }
}
