using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;

namespace ETHShop.Configurations;

public class ShippingAddressConfig : IEntityTypeConfiguration<ShippingAddress>
{
    public void Configure(EntityTypeBuilder<ShippingAddress> builder)
    {
        builder.HasKey(a=>a.AddressID);

        builder.HasOne(a=>a.User)
            .WithMany(a=>a.ShippingAddresses)
            .HasForeignKey(a=>a.AddressID)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
