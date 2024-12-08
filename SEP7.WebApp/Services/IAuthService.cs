using System.Threading.Tasks;
using SEP7.WebApp.Models;

namespace SEP7.WebApp.Services
{
    public interface IAuthService
    {
        Task<User> LoginAsync(User user);
    }
}
