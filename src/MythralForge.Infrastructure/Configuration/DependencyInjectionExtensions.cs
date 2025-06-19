using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}