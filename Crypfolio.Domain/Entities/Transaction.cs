using Crypfolio.Domain.Enums;

namespace Crypfolio.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid AssetId { get; set; } 

    public Guid? WalletId { get; set; }
    public Guid? ExchangeAccountId { get; set; }
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    
    public Asset Asset { get; set; }
}