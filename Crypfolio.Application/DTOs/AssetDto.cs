using Crypfolio.Domain.Entities;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Application.DTOs;

public class AssetDto
{
    public Guid Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public decimal AverageBuyPrice { get; set; }
    public decimal TotalCost => Balance * AverageBuyPrice;
    public decimal UsdValue { get; set; }
    
    public Guid? WalletId { get; set; }
    public Wallet? Wallet { get; set; }

    public Guid? AccountId { get; set; }
    public Account? Account { get; set; }
    
    public AssetSourceType SourceType { get; set; }

    public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
}