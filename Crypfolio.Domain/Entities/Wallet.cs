using Crypfolio.Domain.Entities;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Domain.Entities;

public class Wallet 
{
    public Guid Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } 
    public string AccountName { get; set; } = string.Empty;

    public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    
    public WalletType WalletType { get; set; } = WalletType.Unknown;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}