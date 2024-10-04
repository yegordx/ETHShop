using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;

namespace ETHShop.Configurations;

public class CartItemConfig : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.HasKey(ci => ci.CartItemID);

        builder.HasOne(a => a.ShoppingCart)
            .WithMany(a => a.CartItems)
            .HasForeignKey(a => a.CartID)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(a=>a.Product)
            .WithMany()
            .HasForeignKey(a=>a.ProductID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
