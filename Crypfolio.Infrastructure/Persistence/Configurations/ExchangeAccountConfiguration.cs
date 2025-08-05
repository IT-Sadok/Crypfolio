using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crypfolio.Infrastructure.Persistence.Configurations;

public class ExchangeAccountConfiguration : IEntityTypeConfiguration<ExchangeAccount>
{
    public void Configure(EntityTypeBuilder<ExchangeAccount> builder)
    {
        // Table-per-hierarchy (TPH) inheritance for AccountSource
        builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany<Asset>()
                .WithOne(a => a.ExchangeAccount)
                .HasForeignKey(a => a.ExchangeAccountId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany<Transaction>()
                .WithOne(t => t.ExchangeAccount)
                .HasForeignKey(t => t.ExchangeAccountId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}
