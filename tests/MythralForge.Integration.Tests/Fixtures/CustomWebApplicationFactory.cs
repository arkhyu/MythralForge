
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MySql;
using MythralForge.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace MythralForge.Integration.Tests.Fixtures;
//https://blog.jetbrains.com/dotnet/2023/10/24/how-to-use-testcontainers-with-dotnet-unit-tests/
//https://antondevtips.com/blog/asp-net-core-integration-testing-best-practises
public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MySqlContainer _dbContainer;

    public CustomWebApplicationFactory()
    {
        _dbContainer = new MySqlBuilder()
            .WithImage("mysql:8.0")
            .WithDatabase("testdb")
            .WithUsername("testuser")
            .WithPassword("Test1234!")
            .Build();
    }

    public async Task InitializeAsync()
    {
        // Start the container before the app is built
        await _dbContainer.StartAsync();

        // Trigger app initialization
        using var client = CreateClient(); // This will force the WebHost to build

        // Create a scope to resolve scoped services
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MythralForgeAuthDbContext>();

        var connectionString = _dbContainer.GetConnectionString();
        Console.WriteLine("HAAAAAAAAAAAAA: " + connectionString);
        Console.WriteLine("Connection string: " + connectionString);

        // Apply migrations
        dbContext.Database.Migrate();

        // Seed test user
        await SeedTestUser(scope.ServiceProvider);
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<MythralForgeAuthDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
var connectionString = _dbContainer.GetConnectionString();
        Console.WriteLine("BOBOBO Using DB connection string: " + connectionString); // <- PRINTS WHEN WEB HOST IS BUILT

            services.AddDbContext<MythralForgeAuthDbContext>(options =>
            {
                var connectionString = _dbContainer.GetConnectionString();
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        });
    }

    private async Task SeedTestUser(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        var existingUser = await userManager.FindByEmailAsync("testuser2@example.com");
        if (existingUser == null)
        {
            var testUser = new IdentityUser
            {
                Email = "testuser2@example.com",
                UserName = "testuser2@example.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(testUser, "P@ssw0rd1232!");
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create test user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}
