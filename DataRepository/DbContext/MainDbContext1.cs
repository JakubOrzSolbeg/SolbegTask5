using DataRepository.Entities;
using DataRepository.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataRepository.DbContext;

public class MainDbContext1 : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly IConfiguration _configuration;

    public MainDbContext1(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new BrandConfiguration());

        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                Login = "admin",
                Passhash = "84875ba07b2435a7910cf5098ee92d9caa7d56f161df52336955e91c98f86172",
                Salt = "0e4cbe8a48eb4414a36fbadf88fe1c90",
                UserType = UserType.Admin
            }
            // password zaq12wsx
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MainDb"),
            b => b.MigrationsAssembly(_configuration["MigrationAssembly"]));
    }
}