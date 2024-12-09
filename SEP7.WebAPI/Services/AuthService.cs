using System.ComponentModel.DataAnnotations;
using SEP7.Database.Data;
using SEP7.WebAPI.Models;

namespace SEP7.WebAPI.Services;

public class AuthService : IAuthService
{
    public AuthService(ApplicationDB context)


{
users = context.Users.ToList();
}

private readonly IList<User> users;


public Task<User> UserValidation(string username, string password)
{
    User? existingUser = users.FirstOrDefault(u =>
    
    u.username.Equals(username,StringComparison.OrdinalIgnoreCase)) ?? throw new Exception("User does not exist");

        if (!existingUser.password.Equals(password))
        {
            throw new Exception ("Password was incorrect");
        }

    return Task.FromResult(existingUser);
}
    


}