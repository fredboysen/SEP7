
using System.Security.Claims;

namespace SEP7.WebApp.Services
{
    public interface IAuthService
    {
        public Task LoginAsync(string username, string password);

         public Task LogoutAsync();


         public Task<ClaimsPrincipal> GetAuthAsync();
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    }
}
