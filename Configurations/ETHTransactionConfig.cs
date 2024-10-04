using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ETHShop.Entities;

namespace ETHShop.Configurations;

public class ETHTransactionConfig : IEntityTypeConfiguration<ETHTransaction>
{
    public void Configure(EntityTypeBuilder<ETHTransaction> builder)
    {
        builder.HasKey(a => a.TransactionID);

        builder.HasOne(a=>a.Order)
            .WithOne(a=>a.EthereumTransaction)
            .HasForeignKey<ETHTransaction>(a=>a.OrderID)
            .OnDelete(DeleteBehavior.NoAction);
    }

}
