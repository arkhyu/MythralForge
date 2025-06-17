using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MythralForge.Infrastructure.Persistence;

public static class PersistenceConfigurationExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MythralForgeDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MythralForgeDb")));

        return services;
    }
}
