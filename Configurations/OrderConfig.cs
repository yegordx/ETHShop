using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;

namespace ETHShop.Configurations;

public class OrderConfig  : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(a=>a.OrderID);

        builder.HasOne(a => a.User)
            .WithMany(a => a.Orders)
            .HasForeignKey(a => a.UserID)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a=>a.Seller)
            .WithMany(a=>a.Orders)
            .HasForeignKey(a=>a.SellerID)
            .OnDelete(DeleteBehavior.SetNull);
    }

}
