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
        
        // Relationships
        builder.HasMany(a => a.Assets)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(a => a.Transactions)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
