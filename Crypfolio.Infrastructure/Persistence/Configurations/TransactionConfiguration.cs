using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crypfolio.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        // ExchangeAccount relationship
        builder.HasOne(t => t.ExchangeAccount)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.ExchangeAccountId)
            .OnDelete(DeleteBehavior.SetNull);

        // Wallet relationship
        builder.HasOne(t => t.Wallet)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.WalletId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(t => t.FromAmount).HasPrecision(18, 6);
        builder.Property(t => t.ToAmount).HasPrecision(18, 6);
        builder.Property(t => t.Fee).HasPrecision(18, 6);
        builder.Property(t => t.Price).HasPrecision(18, 6);
    }
}