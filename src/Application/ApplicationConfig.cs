using System.Reflection;
using Application.Additional;
using Application.Additional.Auth;
using Application.Additional.Image;
using Application.Additional.Pet;
using Application.Additional.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationConfig
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<PetAdditional>();
        services.AddScoped<IUserAdditional,UserAdditional>();
        services.AddScoped<IAuthAdditional,AuthAdditional>();
        services.AddScoped<IImageAdditional,ImageAdditional>();
        return services;
    }
}