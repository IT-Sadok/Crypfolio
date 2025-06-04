using Crypfolio.Domain.Abstract;
using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crypfolio.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasOne(t => t.AccountSource)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.AccountSourceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}