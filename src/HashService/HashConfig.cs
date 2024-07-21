using Application.Persistence.Services.HashServices;
using HashService.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HashService;

public static class HashConfig
{
    public static IServiceCollection AddHashService(this IServiceCollection services,IConfiguration configuration)
    {
        int saltLength = int.Parse(configuration.GetSection("HashOptions:SaltLength").Value!);
        int keyLength = int.Parse(configuration.GetSection("HashOptions:KeyLength").Value!);
        int iterations = int.Parse(configuration.GetSection("HashOptions:Iterations").Value!);
        
        HashOptions hashOptions = new HashOptions { SaltLength = saltLength, KeyLength = keyLength, Iterations = iterations };
        services.AddSingleton<IHashService>(_ => new Service.HashService(hashOptions));
        return services;
    }
}