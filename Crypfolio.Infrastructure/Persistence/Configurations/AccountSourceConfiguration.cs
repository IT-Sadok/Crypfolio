using Crypfolio.Domain.Abstract;
using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crypfolio.Infrastructure.Persistence.Configurations;

public class AccountSourceConfiguration : IEntityTypeConfiguration<AccountSource>
{
    public void Configure(EntityTypeBuilder<AccountSource> builder)
    {
        // Table-per-hierarchy (TPH) inheritance for AccountSource
        builder.HasDiscriminator<string>("AccountType")
                .HasValue<Wallet>("Wallet")
                .HasValue<ExchangeAccount>("Exchange");

        builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<Asset>()
                .WithOne(a => a.Wallet)
                .HasForeignKey(a => a.WalletId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<Asset>()
                .WithOne(a => a.ExchangeAccount)
                .HasForeignKey(a => a.ExchangeAccountId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<Transaction>()
                .WithOne(t => t.Wallet)
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<Transaction>()
                .WithOne(t => t.ExchangeAccount)
                .HasForeignKey(t => t.ExchangeAccountId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
