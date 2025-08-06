using Crypfolio.Domain.Enums;

namespace Crypfolio.Application.DTOs;

public class AssetCreateModel
{
    public string Ticker { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal FreeBalance { get; set; }
    public decimal LockedBalance { get; set; }
    public decimal AverageBuyPrice { get; set; }
    public Guid? WalletId { get; set; }
    public Guid? ExchangeAccountId { get; set; }
    public AssetSourceType SourceType { get; set; }
}
