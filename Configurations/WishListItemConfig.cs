using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;

namespace ETHShop.Configurations;

public class WishListItemConfig : IEntityTypeConfiguration<WishListItem>
{
    public void Configure(EntityTypeBuilder<WishListItem> builder)
    {
        builder.HasKey(a => a.WishListItemID);

        builder.HasOne(a=>a.WishList)
            .WithMany(a=>a.WishListItems)
            .HasForeignKey(a=>a.WishListItemID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a=>a.Product)
            .WithMany()
            .HasForeignKey(a=>a.ProductID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
