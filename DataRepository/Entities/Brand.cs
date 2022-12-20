using DataRepository.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = null!;
}

public class BrandConfiguration : BaseEntityTypeConfiguration<Brand>
{
    public override void Configure(EntityTypeBuilder<Brand> builder)
    {
        base.Configure(builder);
    }
}