using System.Security.Claims;
using SEP7.WebAPI.Models;
using SEP7.WebApp.Services;
using Microsoft.AspNetCore.Components.Authorization;

public class CustomAuthProvider : AuthenticationStateProvider
{
     private readonly IAuthService authService;

    public CustomAuthProvider(IAuthService authService)
    {
        this.authService = authService;
        authService.OnAuthStateChanged += AuthStateChanged;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = await authService.GetAuthAsync();

        return new AuthenticationState(principal);
    }

    private void AuthStateChanged(ClaimsPrincipal principal)
    {
        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(principal)
            )
        );
    }

        public void NotifyAuthenticationStateChanged()
    {
       
        var authState = GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(authState);
    }

    public void SetAuthenticatedUser(ClaimsPrincipal user)
    {
        user = user;
        NotifyAuthenticationStateChanged();
    }


    
}