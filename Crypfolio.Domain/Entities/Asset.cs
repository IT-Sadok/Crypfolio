using Crypfolio.Domain.Entities;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Domain.Entities;

public class Asset
{
    public Guid Id { get; set; }
    public string Ticker { get; set; } = string.Empty; 
    public string Name { get; set; } = string.Empty;
    
    public decimal FreeBalance { get; set; }
    public decimal LockedBalance { get; set; }
    public decimal UsdValue { get; set; }
    public decimal AverageBuyPrice { get; set; }
    
    public Guid? WalletId { get; set; }
    public Wallet? Wallet { get; set; }
    public Guid? ExchangeAccountId { get; set; }
    public ExchangeAccount? ExchangeAccount { get; set; }
    
    public AssetSourceType SourceType { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Transaction> TransactionsFrom { get; set; } = new List<Transaction>();
    public ICollection<Transaction> TransactionsTo { get; set; } = new List<Transaction>();
}