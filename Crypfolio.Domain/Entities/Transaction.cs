using Crypfolio.Domain.Abstract;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid AssetId { get; set; } 

    // This replaces both WalletId and ExchangeAccountId
    public Guid AccountSourceId { get; set; }  // required FK
    public AccountSource? AccountSource { get; set; } = null!;
    
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    
    public Asset Asset { get; set; }
}