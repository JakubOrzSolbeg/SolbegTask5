using DataRepository.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PhotoUrl { get; set; } = null!;
    public double Cost { get; set; }
    public int BrandId { get; set; } = 1;
    public Brand ProductBrand { get; set; } = null!;

    public int CategoryId { get; set; } = 1;

    public Category ProductCategory { get; set; } = null!;
    
    public ICollection<OrderProduct> OrderProducts { get; set; }

}

public class ProductConfiguration : BaseEntityTypeConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        builder
            .HasOne<Brand>(brand => brand.ProductBrand)
            .WithMany(brand => brand.Products)
            .HasForeignKey(product => product.BrandId);
        builder.HasOne<Category>(p => p.ProductCategory)
            .WithMany(category => category.Products)
            .HasForeignKey(p => p.CategoryId);
    }
}