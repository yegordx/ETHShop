using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;

namespace ETHShop.Configurations;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(a => a.OrderItemID);

        builder.HasOne(a=>a.Order)
            .WithMany(a=>a.OrderItems)
            .HasForeignKey(a=>a.OrderID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a=>a.Product)
            .WithMany()
            .HasForeignKey(a=>a.ProductID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
