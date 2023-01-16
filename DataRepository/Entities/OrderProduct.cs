using DataRepository.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class OrderProduct : BaseEntity
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    
    public virtual Product Product { get; set; }
    public virtual Order Order { get; set; }
}

public class OrderProductConfiguration : BaseEntityTypeConfiguration<OrderProduct>
{
    public override void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        base.Configure(builder);
        builder.HasOne<Product>(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);

        builder.HasOne<Order>(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);
    }
}