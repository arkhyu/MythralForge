/*using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.MsSql;
using MythralForge.Infrastructure.Persistence;

namespace MythralForge.Integration.Tests.Fixtures;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private MsSqlContainer _dbContainer = default!;
    public string ConnectionString => _dbContainer.GetConnectionString();

    public async Task InitializeAsync()
    {
        _dbContainer = new MsSqlBuilder()
            .WithPassword("P@ssword123!")
            .Build();
        Console.WriteLine("Starting StartAsync...");
        await _dbContainer.StartAsync();
        Console.WriteLine("After StartAsync...");
        using var scope = Services.CreateScope();
        Console.WriteLine("After scope...");
        var db = scope.ServiceProvider.GetRequiredService<MythralForgeAuthDbContext>();
        Console.WriteLine("before migrate ...");

        await db.Database.MigrateAsync();
        Console.WriteLine("after migrate ...");
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
            options.UseMySql(
               ConnectionString,
                ServerVersion.AutoDetect(ConnectionString)
            ));
        });
    }
}
*/