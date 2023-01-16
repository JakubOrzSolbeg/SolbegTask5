using DataRepository.Entities.Base;
using Dtos.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataRepository.Entities;

public class User : BaseEntity
{
    public string Login { get; set; } = null!;
    public string Passhash { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public UserType UserType { get; set; } = UserType.User;
    
    public virtual ICollection<Order> Orders { get; set; }
}

public class UserConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.HasIndex(user => user.Login).IsUnique();
    }
}