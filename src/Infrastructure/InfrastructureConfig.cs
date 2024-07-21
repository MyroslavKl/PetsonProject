using Application.Persistence.Repositories;
using Application.Persistence.Repositories.Common;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureConfig
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PetsonContext>(options => {
            options.UseNpgsql(configuration.GetConnectionString("Database"));
        });
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IImageRepository,ImageRepository>();
        services.AddScoped<IReserveRepository,ReserveRepository>();
        services.AddScoped<IRoleRepository,RoleRepository>();
        services.AddScoped<IUserRepository,UserRepository>();
        return services;
    }
}