using System.Net;
using System.Net.Http.Json;
using Crypfolio.API.Constants;
using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;
using Crypfolio.Domain.Enums;
using Crypfolio.Infrastructure.Persistence;
using Crypfolio.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Crypfolio.IntegrationTests.Tests;

public class ExchangeAccountApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly IServiceScopeFactory _scopeFactory;
    
    public ExchangeAccountApiTests(CustomWebApplicationFactory factory)
    {
        _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        _client = factory.CreateClient();
    }
    private async Task<ApplicationUser> GetTestUserAsync()
    {
        await using var scope = _scopeFactory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await TestUserFactory.GetOrCreateTestUserAsync(db);
    }
    
    [Fact]
    public async Task CreateExchangeAccount_ReturnsSuccessAndPersists()
    {
        // Arrange
        var user = await GetTestUserAsync();
        
        var dto = new ExchangeAccountCreateModel
        {
            UserId = user.Id,
            AccountName = "Binance Test Account",
            ExchangeName = ExchangeName.Binance,
            ApiKey = "test-api-key",
            ApiSecret = "test-api-secret"
        };
        
        // Act
        var response = await _client.PostAsJsonAsync(Routes.ExchangeAccounts, dto);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ExchangeAccountDto>();

        result.Should().NotBeNull();
        result!.AccountName.Should().Be(dto.AccountName);
        result.ExchangeName.Should().Be(dto.ExchangeName);
    }

    [Fact]
    public async Task GetAllExchangeAccounts_ReturnsListContainingCreated()
    {
        // Arrange
        var user = await GetTestUserAsync();
        
        var dto = new ExchangeAccountCreateModel
        {
            UserId = user.Id,
            AccountName = "Test Account",
            ExchangeName = ExchangeName.Binance,
            ApiKey = "api-key",
            ApiSecret = "api-secret"
        };

        var postResponse = await _client.PostAsJsonAsync(Routes.ExchangeAccounts, dto);
        postResponse.EnsureSuccessStatusCode();
        
        // Act
        var getResponse = await _client.GetAsync(Routes.ExchangeAccountsByUserId + $"?userId={dto.UserId}&pageNumber=1&pageSize=10");
        
        // Assert
        getResponse.EnsureSuccessStatusCode();
        var result = await getResponse.Content.ReadFromJsonAsync<List<ExchangeAccountDto>>();

        result.Should().NotBeNull();
        result.Should().ContainSingle(x => x.AccountName == dto.AccountName);
    }

    [Fact]
    public async Task GetExchangeAccountById_ReturnsCorrectItem()
    {
        // Arrange
        var user = await GetTestUserAsync();

        var dto = new ExchangeAccountCreateModel
        {
            UserId = user.Id,
            AccountName = "Account To Retrieve",
            ExchangeName = ExchangeName.Binance,
            ApiKey = "key",
            ApiSecret = "secret"
        };

        var createResponse = await _client.PostAsJsonAsync(Routes.ExchangeAccounts, dto);
        
        // Assert
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
        var user = await GetTestUserAsync();

        var dto = new ExchangeAccountCreateModel
        {
            UserId = user.Id,
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
