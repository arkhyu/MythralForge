/*using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MythralForge.Infrastructure.Persistence;
using Testcontainers.MySql;
public class MyTest : IAsyncLifetime
{
    private IServiceProvider Services;
    private MySqlContainer _container;
  private readonly HttpClient _client;
    public MyTest()
    {
         _container = new MySqlBuilder()
            .WithImage("mysql:8.0")                // specify the version you want
            .WithDatabase("testdb")                // default database
            .WithUsername("testuser")             // optional, default is "mysql"
            .WithPassword("Test1234!")            // required
            .Build();
        Services = RegisteredServices();
    }

[Fact]
    public async Task Register_ReturnsOk_WhenUserIsValid()
    {
        var content = "regis";
        Assert.Contains("User registered successfully", content);
    }
    public async Task DisposeAsync()
    {
        await _container.StopAsync();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        var context = Services.GetRequiredService<MythralForgeAuthDbContext>();
        context.Database.EnsureCreated();
    }

    private IServiceProvider? RegisteredServices()
    {
        var col = new ServiceCollection();
        col.AddTransient<IConfiguration>(_ =>
        {
            var configs = new List<KeyValuePair<string, string>>
            {
                new ("ConnectionStrings:MythralForgeAuthDb",_container.GetConnectionString())
            };

            return new ConfigurationBuilder()
            .AddInMemoryCollection(configs)
            .Build();
        });
        col.AddDbContext<MythralForgeAuthDbContext>(options =>
        {
            var configuration = col.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("MythralForgeAuthDb");

            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
             col.AddScoped<RegisterUserHandler>();
        col.AddScoped<LoginHandler>();
        col.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MythralForgeAuthDbContext>()
                .AddDefaultTokenProviders();

        // This configuration ensures that during user registration, 
        // any password that doesn’t meet the defined complexity requirements is rejected.
        // Additionally, during login, if a user enters the wrong password too many times, 
        // their account will be temporarily locked to protect against unauthorized access.
        col.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
        });
        col.AddScoped<IAuthenticationService, AuthenticationService>();
        
        return col.BuildServiceProvider();
    }
}*/