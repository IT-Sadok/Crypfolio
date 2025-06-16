namespace Crypfolio.Application.DTOs;

public class AssetBalanceDto
{
    public string Ticker { get; set; }
    public decimal Free { get; set; }
    public decimal Locked { get; set; }
}