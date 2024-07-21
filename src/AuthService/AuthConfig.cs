using System.Reflection;
using Application.Persistence.Services.AuthServices;
using HashService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService;

public static class AuthConfig
{
    public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHashService(configuration);
        services.AddScoped<IAuthService, Service.AuthService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}