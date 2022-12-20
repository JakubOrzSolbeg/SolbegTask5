using DataRepository.DbContext;
using DataRepository.Entities;
using DataRepository.Repositories.Implementations;
using DataRepository.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DataRepository;

public static class Setup
{
    public static IServiceCollection AddDataRepository(this IServiceCollection services)
    {
        services.AddDbContext<MainDbContext1>();
        services.AddScoped<Repository<User>, AccountRepository>();
        services.AddScoped<Repository<Product>, ProductRepository>();
        services.AddScoped<Repository<Brand>, BrandRepository>();
        return services;
    }
}