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
        services.AddScoped<ExtendedRepository<User>>();
        services.AddScoped<ExtendedRepository<Product>>();
        services.AddScoped<ExtendedRepository<Category>>();
        services.AddScoped<ExtendedRepository<Brand>>();
        services.AddScoped<NestedRepository<Order>, OrderRepository>();
        return services;
    }
}