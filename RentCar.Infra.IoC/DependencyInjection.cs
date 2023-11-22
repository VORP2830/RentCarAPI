using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentCar.Application.DTOs.Mapping;
using RentCar.Application.Interfaces;
using RentCar.Application.Services;
using RentCar.Domain.Interfaces;
using RentCar.Infra.Data.Repositories;
using VoteAPI.Infra.Data.Context;

namespace RentCar.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE") ?? configuration.GetConnectionString("DefaultConnection");
        service.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        service.AddAutoMapper(typeof(MappingProfile));

        service.AddScoped<IUnitOfWork, UnitOfWork>();

        service.AddScoped<IClientService, ClientService>();
        service.AddScoped<ICarService, CarService>();
        service.AddScoped<IRentService, RentService>();

        return service;
    }
}
