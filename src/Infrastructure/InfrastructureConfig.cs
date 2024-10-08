﻿using Application.Persistence.Repositories;
using Application.Persistence.Repositories.Common;
using Application.Persistence.Services;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Common;
using Infrastructure.Persistence.Services;
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
        services.AddScoped<IPetRepository,PetRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPetService, PetService>();
        services.AddScoped<IReserveService, ReserveService>();
        services.AddScoped<IImageService, ImageService>();
        return services;
    }
}