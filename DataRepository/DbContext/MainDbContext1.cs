using DataRepository.Entities;
using Dtos.Enums;
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
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new BrandConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new OrderProductConfiguration());

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

        modelBuilder.Entity<Category>().HasData(
            new Category()
            {
                Id = 1,
                Name = "Laptop"
            },
            new Category()
            {
                Id = 2,
                Name = "Smartphone"
            }
        );

        modelBuilder.Entity<Brand>().HasData(
            new Brand()
            {
                Id = 1,
                Name = "Lenovo"
            },
            new Brand()
            {
                Id = 2,
                Name = "Samsung"
            });

        modelBuilder.Entity<Product>().HasData(
            new Product()
            {
                Name = "Samgung A20",
                Description = "Good looking smartphone",
                BrandId = 2,
                CategoryId = 2,
                PhotoUrl = "dadasdsdasd"
            },
            new Product()
            {
                Name = "Samgung Galaxy 4",
                Description = "Model brand phone",
                BrandId = 2,
                CategoryId = 2,
                PhotoUrl = "idknlinknoeworkinda"
            });

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MainDb"),
            b => b.MigrationsAssembly(_configuration["MigrationAssembly"]));
    }
}