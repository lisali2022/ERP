using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddOneAuth(this IServiceCollection services)
    {
        services.AddAuthentication()
            .AddCookie(options => options.LoginPath = "/login");
        return services;
    }

    public static IApplicationBuilder UseOneAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}
