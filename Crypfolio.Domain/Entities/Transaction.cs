using Crypfolio.Domain.Entities;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }

    public string? FromAsset { get; set; }
    public string? ToAsset { get; set; }

    public decimal FromAmount { get; set; }
    public decimal ToAmount { get; set; }

    public string? FromAddress { get; set; } 
    public string? ToAddress { get; set; } 
    
    public decimal Price { get; set; }
    public decimal Fee { get; set; }
    public string? FeeAsset { get; set; }

    public TransactionStatus? Status { get; set; }
    
    public Guid? WalletId { get; set; }
    public Wallet? Wallet { get; set; }
    public string? WalletName { get; set; }
    
    public Guid? ExchangeAccountId { get; set; }
    public ExchangeAccount? ExchangeAccount { get; set; }
    public string? ExchangeAccountName { get; set; }
    public TransactionType Type { get; set; } 

    public string? BlockchainTxHash { get; set; }
    public string? ExchangeOrderId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}