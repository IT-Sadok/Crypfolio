using System.Net.Http.Json;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Infrastructure.Services;

public class BinanceApiService : IExchangeApiService
{
    private readonly HttpClient _httpClient;
    public ExchangeName ExchangeName => ExchangeName.Binance;
    
    public BinanceApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.binance.com/");
    }

    public async Task<List<AssetDto>> GetAvailableAssetsAsync(string apiKey, string secretKey, CancellationToken ct)
    {
        var result = await _httpClient.GetFromJsonAsync<BinanceAccountInfoDto>("api/v3/account", ct);

        return result?.Balances
            .Where(b => decimal.Parse(b.Free) > 0 || decimal.Parse(b.Locked) > 0)
            .Select(b => new AssetDto
            {
                Ticker = b.Asset,
                FreeBalance = decimal.Parse(b.Free),
                LockedBalance = decimal.Parse(b.Locked)
            }).ToList() ?? new();
    }
}

public class BinanceAccountInfoDto
{
    public List<BinanceBalanceDto> Balances { get; set; } = new();
}

public class BinanceBalanceDto
{
    public string Asset { get; set; } = default!;
    public string Free { get; set; } = default!;
    public string Locked { get; set; } = default!;
}