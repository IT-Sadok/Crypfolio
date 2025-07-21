using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Crypfolio.Application.DTOs;
using Crypfolio.Infrastructure.Services;
using FluentAssertions;
using Xunit;

namespace Crypfolio.IntegrationTests.Tests.Services;

public class BinanceApiServiceTests
{
    [Fact]
    public async Task GetAvailableAssetsAsync_ReturnsExpectedAssets()
    {
        // Arrange: fake response that Binance would return
        var binanceResponse = new BinanceAccountInfoDto
        {
            Balances = new List<BinanceBalanceDto>
            {
                new() { Asset = "BTC", Free = "0.1", Locked = "0.2" },
                new() { Asset = "ETH", Free = "0.0", Locked = "1.0" },
                new() { Asset = "SOL", Free = "0.0", Locked = "0.0" }, // should be filtered out
            }
        };

        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(binanceResponse)
        };

        var handler = new MockHttpMessageHandler(mockResponse);
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.binance.com/")
        };

        var service = new BinanceApiService(httpClient);

        // Act
        var result = await service.GetAvailableAssetsAsync("fake-key", "fake-secret", CancellationToken.None);

        // Assert
        result.Should().HaveCount(2);

        result.Should().ContainEquivalentOf(new AssetDto
        {
            Ticker = "BTC",
            FreeBalance = 0.1m,
            LockedBalance = 0.2m
        }, options => options.Excluding(x => x.RetrievedAt));

        result.Should().ContainEquivalentOf(new AssetDto
        {
            Ticker = "ETH",
            FreeBalance = 0.0m,
            LockedBalance = 1.0m
        }, options => options.Excluding(x => x.RetrievedAt));
    }
}