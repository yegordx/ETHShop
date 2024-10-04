using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;

namespace ETHShop.Configurations;

public class WishListConfig : IEntityTypeConfiguration<WishList>
{
    public void Configure(EntityTypeBuilder<WishList> builder)
    {
        builder.HasKey(a=>a.WishListID);

        builder.HasOne(a=>a.User)
            .WithMany(a=>a.WishLists)
            .HasForeignKey(a=>a.UserID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
