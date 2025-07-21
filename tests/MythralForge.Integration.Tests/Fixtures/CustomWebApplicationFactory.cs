
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.MySql;
using MythralForge.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;

namespace MythralForge.Integration.Tests.Fixtures;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MySqlContainer _dbContainer;
    public CustomWebApplicationFactory()
    {
        this._dbContainer = new MySqlBuilder()
                   .WithImage("mysql:8.0")                
                   .WithDatabase("testdb")                
                   .WithUsername("testuser")             
                   .WithPassword("Test1234!")            
                   .Build();

    }
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
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
            if (descriptor != null) services.Remove(descriptor);

            services.AddDbContext<MythralForgeAuthDbContext>(options =>
            {
                var connectionString = _dbContainer.GetConnectionString();
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            // Ensure the database is created and migrations are applied
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MythralForgeAuthDbContext>();
            dbContext.Database.Migrate();
        });
    }
}
