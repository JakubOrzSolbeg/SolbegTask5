using DataRepository.Entities.Base;
using Dtos.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public DateTime OrderTime { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public virtual User User { get; set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }

}

public class OrderConfiguration : BaseEntityTypeConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.OrderTime).HasDefaultValue(DateTime.Now);
        builder.HasOne<User>(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
    }
}