using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CacheServices;

public static class CacheConfig
{
    public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
    {

        return services;
    }
}