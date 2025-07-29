using Crypfolio.Domain.Abstract;
using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crypfolio.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        // Asset relationships
        builder.HasOne(t => t.FromAsset)
            .WithMany(a => a.TransactionsFrom)
            .HasForeignKey(t => t.FromAssetId)
            .OnDelete(DeleteBehavior.Restrict); // To keep the transaction

        builder.HasOne(t => t.ToAsset)
            .WithMany(a => a.TransactionsTo)
            .HasForeignKey(t => t.ToAssetId)
            .OnDelete(DeleteBehavior.Restrict);

        // ExchangeAccount relationship
        builder.HasOne(t => t.ExchangeAccount)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.ExchangeAccountId)
            .OnDelete(DeleteBehavior.Restrict);

        // Wallet relationship
        builder.HasOne(t => t.Wallet)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.WalletId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(t => t.FromAmount).HasPrecision(18, 6);
        builder.Property(t => t.ToAmount).HasPrecision(18, 6);
    }
}