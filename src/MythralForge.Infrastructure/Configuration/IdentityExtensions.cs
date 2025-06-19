using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MythralForge.Infrastructure.Persistence;

public static class IdentityExtensions
{
    public static async Task SeedRolesAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        await RoleInitializer.SeedRolesAsync(services);
    }
    public static IServiceCollection AddConfiguredIdentity(this IServiceCollection services)
    {
        // Registers ASP.NET Core Identity services with default user and role types.
        // - Uses IdentityUser as the user entity and IdentityRole for role management.
        // - Configures Entity Framework Core to persist Identity data in the MythralForgeAuthDbContext database.
        // - Adds default token providers for account confirmation, password reset, and two-factor authentication.
        services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MythralForgeAuthDbContext>()
                .AddDefaultTokenProviders();

        // This configuration ensures that during user registration, 
        // any password that doesn’t meet the defined complexity requirements is rejected.
        // Additionally, during login, if a user enters the wrong password too many times, 
        // their account will be temporarily locked to protect against unauthorized access.
        services.Configure<IdentityOptions>(options =>
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

        return services;
    }


}