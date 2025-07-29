using Crypfolio.Domain.Abstract;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }

    public Guid? FromAssetId { get; set; }
    public Asset? FromAsset { get; set; }

    public Guid? ToAssetId { get; set; }
    public Asset? ToAsset { get; set; }

    public decimal FromAmount { get; set; }
    public decimal ToAmount { get; set; }

    public Guid? WalletId { get; set; }
    public Wallet? Wallet { get; set; }

    public Guid? ExchangeAccountId { get; set; }
    public ExchangeAccount? ExchangeAccount { get; set; }

    public TransactionType Type { get; set; } 

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}