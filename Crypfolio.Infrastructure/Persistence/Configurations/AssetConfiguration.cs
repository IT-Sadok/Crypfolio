using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crypfolio.Infrastructure.Persistence.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).HasDefaultValueSql("NEWID()");
        builder.Property(a => a.Name).IsRequired();

        builder.Property(a => a.AverageBuyPrice).HasPrecision(18, 4);
        builder.Property(a => a.FreeBalance).HasPrecision(18, 6);
        builder.Property(a => a.LockedBalance).HasPrecision(18, 6);
        builder.Property(a => a.UsdValue).HasPrecision(18, 2);
    }
}
