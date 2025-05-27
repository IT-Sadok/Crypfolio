using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Crypfolio.Domain.Entities;

namespace Crypfolio.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Asset> Assets { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    //public DbSet<Wallet> Wallets { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optional: Customize column types here
        modelBuilder.Entity<Asset>()
            .Property(a => a.AverageBuyPrice)
            .HasPrecision(18, 4);
        modelBuilder.Entity<Asset>()
            .Property(a => a.Balance)
            .HasPrecision(18, 6);
        modelBuilder.Entity<Asset>()
            .Property(a => a.UsdValue)
            .HasPrecision(18, 2);
    }
}