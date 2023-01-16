using DataRepository.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Product> Products { get; set; }
}

public class CategoryConfiguration : BaseEntityTypeConfiguration<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);
        builder.Property(p => p.Name).HasDefaultValue("Unknown");
    }
}