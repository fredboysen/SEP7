using SEP7.WebApp.Components;
using MudBlazor.Services; // Add MudBlazor namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add MudBlazor services
builder.Services.AddMudServices(); 

// Add HttpClient for API interaction
builder.Services.AddHttpClient("WebAPI", client => { 
    client.BaseAddress = new Uri("https://localhost:7073");
});

builder.Services.AddHttpClient();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins("https://localhost:5123")  
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseCors("AllowAll"); // Apply the CORS policy
app.UseHttpsRedirection();
app.UseAntiforgery(); // Ensure it does not block your upload requests
app.UseStaticFiles();
app.MapStaticAssets();
app.MapRazorComponents<SEP7.WebApp.Components.App>()
    .AddInteractiveServerRenderMode();

app.Run();
