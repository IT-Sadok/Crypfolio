using Crypfolio.Domain.Abstract;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Domain.Entities;

public class Asset
{
    public Guid Id { get; set; }
    public string Ticker { get; set; } = string.Empty; 
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public decimal UsdValue { get; set; }
    
    public decimal AverageBuyPrice { get; set; }
    public Guid? WalletId { get; set; }
    public AccountSource? Wallet { get; set; }

    public Guid? ExchangeAccountId { get; set; }
    public AccountSource? ExchangeAccount { get; set; }
    
    public AssetSourceType SourceType { get; set; }
    public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<Transaction> TransactionsFrom { get; set; } = new List<Transaction>();
    public ICollection<Transaction> TransactionsTo { get; set; } = new List<Transaction>();
}