using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MythralForge.Infrastructure.Persistence;

public static class PersistenceConfigurationExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MythralForgeDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("MythralForgeDb"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("MythralForgeDb"))
            ));

        services.AddDbContext<MythralForgeAuthDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("MythralForgeAuthDb"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("MythralForgeAuthDb"))
            ));
                // Add Identity with EF Core stores
                services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddEntityFrameworkStores<MythralForgeAuthDbContext>()
                        .AddDefaultTokenProviders();
                
        return services;
    }
}
