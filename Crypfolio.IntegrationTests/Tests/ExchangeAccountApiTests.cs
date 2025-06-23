using System.Net;
using System.Net.Http.Json;
using Crypfolio.API.Constants;
using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Enums;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Crypfolio.IntegrationTests.Tests;

public class ExchangeAccountApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;
    
    public ExchangeAccountApiTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateExchangeAccount_ReturnsSuccessAndPersists()
    {
        // Arrange
        var expectedAssets = new List<AssetBalanceDto> 
        { 
            new() { Ticker = "BTC", FreeBalance = 0.5m}, 
            new() { Ticker = "ETH", FreeBalance = 1.2m }
        };
        
        _factory.BinanceApiMock
            .GetAvailableAssetsAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(expectedAssets);
        
        var dto = new ExchangeAccountCreateDto
        {
            UserId = Guid.NewGuid().ToString(),
            AccountName = "Binance Test Account",
            ExchangeName = ExchangeName.Binance,
            ApiKey = "test-api-key",
            ApiSecret = "test-api-secret"
        };
        
        // Act
        var response = await _client.PostAsJsonAsync(Routes.ExchangeAccounts, dto);

        // Assert
        response.EnsureSuccessStatusCode();

        await _factory.BinanceApiMock
            .Received(1)
            .GetAvailableAssetsAsync(dto.ApiKey, dto.ApiSecret, Arg.Any<CancellationToken>());
        
        // Assert
        var result = await response.Content.ReadFromJsonAsync<ExchangeAccountDto>();

        result.Should().NotBeNull();
        result!.AccountName.Should().Be(dto.AccountName);
        result.ExchangeName.Should().Be(dto.ExchangeName);
    }

    [Fact]
    public async Task GetAllExchangeAccounts_ReturnsListContainingCreated()
    {
        // Arrange
        var dto = new ExchangeAccountCreateDto
        {
            UserId = Guid.NewGuid().ToString(),
            AccountName = "Test Account",
            ExchangeName = ExchangeName.Binance,
            ApiKey = "api-key",
            ApiSecret = "api-secret"
        };

        await _client.PostAsJsonAsync(Routes.ExchangeAccounts, dto);

        // Act
        var response = await _client.GetAsync(Routes.ExchangeAccounts);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<List<ExchangeAccountDto>>();

        result.Should().NotBeNull();
        result.Should().ContainSingle(x => x.AccountName == dto.AccountName);
    }

    [Fact]
    public async Task GetExchangeAccountById_ReturnsCorrectItem()
    {
        // Arrange
        var dto = new ExchangeAccountCreateDto
        {
            UserId = Guid.NewGuid().ToString(),
            AccountName = "Account To Retrieve",
            ExchangeName = ExchangeName.Binance,
            ApiKey = "key",
            ApiSecret = "secret"
        };

        var createResponse = await _client.PostAsJsonAsync(Routes.ExchangeAccounts, dto);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<ExchangeAccountDto>();

        // Act
        var getResponse = await _client.GetAsync($"{Routes.ExchangeAccounts}/{created!.Id}");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var result = await getResponse.Content.ReadFromJsonAsync<ExchangeAccountDto>();

        result.Should().NotBeNull();
        result!.Id.Should().Be(created.Id);
        result.AccountName.Should().Be(dto.AccountName);
    }

    [Fact]
    public async Task DeleteExchangeAccount_RemovesSuccessfully()
    {
        // Arrange
        var dto = new ExchangeAccountCreateDto
        {
            UserId = Guid.NewGuid().ToString(),
            AccountName = "Delete Me",
            ExchangeName = ExchangeName.Binance,
            ApiKey = "key",
            ApiSecret = "secret"
        };

        var createResponse = await _client.PostAsJsonAsync(Routes.ExchangeAccounts, dto);
        createResponse.EnsureSuccessStatusCode();
        var created = await createResponse.Content.ReadFromJsonAsync<ExchangeAccountDto>();

        // Act: delete
        var deleteResponse = await _client.DeleteAsync($"{Routes.ExchangeAccounts}/{created!.Id}");

        // Assert
        deleteResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"{Routes.ExchangeAccounts}/{created.Id}");
        getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
