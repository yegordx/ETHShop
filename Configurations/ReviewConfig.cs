using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;

namespace ETHShop.Configurations;

public class ReviewConfig : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(a=>a.ReviewID);

        builder.HasOne(a=>a.Product)
            .WithMany(a=>a.Reviews)
            .HasForeignKey(a=>a.ProductID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a=>a.User)
            .WithMany(a=>a.Reviews)
            .HasForeignKey(a=>a.UserID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
