using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;

namespace ETHShop.Configurations;

public class NotificationConfig : IEntityTypeConfiguration<Notification>
{
    public void Configure (EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasOne(a=>a.User)
            .WithMany(a=>a.Notifications)
            .HasForeignKey(a=>a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
