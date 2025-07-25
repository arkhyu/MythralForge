using Microsoft.Extensions.DependencyInjection;
using MythralForge.Application.Auth.Login;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Register your use case handlers here
        services.AddScoped<RegisterUserHandler>();
        services.AddScoped<LoginHandler>();
        return services;
    }
}