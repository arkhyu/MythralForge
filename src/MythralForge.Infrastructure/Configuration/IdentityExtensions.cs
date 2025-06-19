using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class IdentityExtensions
{
    public static async Task SeedRolesAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        await RoleInitializer.SeedRolesAsync(services);
    }
}