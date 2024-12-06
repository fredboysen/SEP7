using Microsoft.EntityFrameworkCore;
using SEP7.Database.Data; 
using Microsoft.OpenApi.Models; 
using SEP7.WebAPI.Data;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddControllers();  

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

    builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.MaxDepth = 32; // or any other appropriate depth
    });


builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>

{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SEP7 API", Version = "v1" });
});


builder.Services.AddDbContext<ApplicationDB>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SEP7 API v1");
        c.RoutePrefix = string.Empty;  
    });
}
}


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDB>();
    await DbSeeder.Seed(context);  
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapControllers();

app.Run();

