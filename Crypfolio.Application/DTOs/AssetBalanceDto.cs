namespace Crypfolio.Application.DTOs;

public class AssetBalanceDto
{
    public string Ticker { get; set; }
    public decimal FreeBalance { get; set; }
    public decimal LockedBalance { get; set; }
}