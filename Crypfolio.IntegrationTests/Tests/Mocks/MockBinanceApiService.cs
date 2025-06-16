using Crypfolio.Application.Interfaces;
using Crypfolio.Application.DTOs;

namespace Crypfolio.IntegrationTests.Tests.Mocks;

public class MockBinanceApiService : IBinanceApiService
{
    public Task<List<AssetBalanceDto>> GetAccountBalancesAsync(
        string apiKey, string apiSecret, CancellationToken cancellationToken)
    {
        var balances = new List<AssetBalanceDto>
        {
            new AssetBalanceDto { Ticker = "BTC", Free = 0.25m, Locked = 0.05m },
            new AssetBalanceDto { Ticker = "ETH", Free = 2.0m, Locked = 0.0m }
        };
        return Task.FromResult(balances);
    }
}
