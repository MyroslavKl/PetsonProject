using CacheServices.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CacheServices;

public static class CacheConfig
{
    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICacheService,CacheService>();
        return services;
    }
}