using System;
using FilamentCalculator.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// DbContext Konfiguration
builder.Services.AddDbContext<FilamentCalcContext>(options =>
{
    var connectionString = $"Host={Environment.GetEnvironmentVariable("PGHostname")};" +
                         $"Database={Environment.GetEnvironmentVariable("PGDB")};" +
                         $"Username={Environment.GetEnvironmentVariable("PGUsername")};" +
                         $"Password={Environment.GetEnvironmentVariable("PGPassword")}";
    
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initialize Database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<FilamentCalcContext>();
        context.Database.Migrate();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ein Fehler ist bei der Datenbankinitialisierung aufgetreten.");
    }
}

app.Run();