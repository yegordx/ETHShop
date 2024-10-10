using ETHShop.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ETHShop.Configurations;

public class SellerConfig : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.HasKey(s => s.SellerID);

        builder.HasOne(s => s.User)
            .WithOne(u => u.Seller)
            .HasForeignKey<Seller>(s => s.UserID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s=>s.Products)
            .WithOne(s=>s.Seller)
            .HasForeignKey(s => s.SellerID)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
