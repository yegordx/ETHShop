using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;

namespace ETHShop.Configurations;

public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(a=>a.PaymentID);

        builder.HasOne(a=>a.Order)
            .WithOne(a=>a.Payment)
            .HasForeignKey<Payment>(a=>a.OrderID)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
