using System.Security.Claims;

namespace SEP7.WebAPI.Auth
{
    public static class AuthorizationPolicies
    {
        public static void AddPolicies(IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("User", a =>
                    a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "User"));

                options.AddPolicy("Admin", a =>
                    a.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "Admin"));
            });
        }
    }
}
