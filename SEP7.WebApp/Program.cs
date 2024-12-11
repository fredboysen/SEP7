using SEP7.WebApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using SEP7.WebAPI.Auth;
using System.Security.Claims;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
    

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7073/") });

builder.Services.AddCors();

builder.Services.AddScoped<Radzen.TooltipService>();
builder.Services.AddScoped<Radzen.DialogService>();
builder.Services.AddScoped<Radzen.NotificationService>();

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("Admin", policy =>
        policy.RequireAuthenticatedUser().RequireClaim(ClaimTypes.Role, "Admin"));
});
builder.Services.AddAuthentication().AddCookie(options =>
{
    options.LoginPath = "/login";
});



builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
AuthorizationPolicies.AddPolicies(builder.Services);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseAuthorization();
app.UseCors("AllowAll"); // Apply the CORS policy
app.UseHttpsRedirection();
app.UseAntiforgery(); // Ensure it does not block your upload requests



app.UseStaticFiles();
app.MapStaticAssets();
app.MapRazorComponents<SEP7.WebApp.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
