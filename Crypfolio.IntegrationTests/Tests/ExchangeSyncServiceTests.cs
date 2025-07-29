using System.Net;
using System.Net.Http.Json;
using Crypfolio.API.Constants;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Crypfolio.Application.Services;
using Crypfolio.Domain.Entities;
using Crypfolio.Domain.Enums;
using Crypfolio.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Crypfolio.IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using JsonSerializerOptions = System.Text.Json.JsonSerializerOptions;

namespace Crypfolio.IntegrationTests.Tests;

public class ExchangeSyncServiceTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly HttpClient _client;

    public ExchangeSyncServiceTests(CustomWebApplicationFactory factory)
    {
        _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        _client = factory.CreateClient();
    }

    private async Task<ApplicationDbContext> GetDbContextAsync()
    {
        var scope = _scopeFactory.CreateScope();
        return scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    private IExchangeSyncService GetSyncService()
    {
        var scope = _scopeFactory.CreateScope();
        return scope.ServiceProvider.GetRequiredService<IExchangeSyncService>();
    }

    [Fact]
    public async Task SyncAccountAsync_AddsOrUpdatesAssets()
    {
        await using var db = await GetDbContextAsync();
        var syncService = GetSyncService();

        var user = await TestUserFactory.GetOrCreateTestUserAsync(db);

        var account = new ExchangeAccount
        {
            AccountName = "Test Binance Account",
            UserId = user.Id,
            User = user,
            ApiKeyEncrypted = "dummy-key",
            ApiSecretEncrypted = "dummy-secret",
            ExchangeName = ExchangeName.Binance
        };

        var createResponse = await _client.PostAsJsonAsync(Routes.ExchangeAccounts, account);
        createResponse.EnsureSuccessStatusCode();

        var createdAccount = await createResponse.Content.ReadFromJsonAsync<ExchangeAccountDto>();
        createdAccount.Should().NotBeNull();

        // Add fake assets
        var newAssets = new List<AssetCreateDto>
        {
            new() { Ticker = "BTC", Balance = 0.5m, ExchangeAccountId = createdAccount.Id },
            new() { Ticker = "SOL", Balance = 4.5m, ExchangeAccountId = createdAccount.Id }
        };

        // Act
        foreach (var asset in newAssets)
        {
            var addAssetsResponse = await _client.PostAsJsonAsync(Routes.Assets, asset);
            addAssetsResponse.EnsureSuccessStatusCode();
        }

        await syncService.SyncAccountAsync(account, CancellationToken.None);

        var getAssetsResponse =
            await _client.GetAsync(Routes.AssetsByAccountSourceId + $"?id={createdAccount.Id}");
        getAssetsResponse.EnsureSuccessStatusCode();
        var syncedAssets = await getAssetsResponse.Content.ReadFromJsonAsync<List<AssetDto>>();

        // Assert
        syncedAssets.Should().NotBeNullOrEmpty();
        syncedAssets.Should().HaveCount(2);
        syncedAssets.Should().OnlyContain(a => a.ExchangeAccountId == createdAccount.Id);
        syncedAssets.Should().Contain(a => a.Ticker == "BTC" && a.FreeBalance + a.LockedBalance == 0.5m);
        syncedAssets.Should().Contain(a => a.Ticker == "SOL" && a.FreeBalance + a.LockedBalance == 4.5m);
    }

    [Fact]
    public async Task Should_Delete_ExchangeAccount_And_Cascade_Delete_Assets()
    {
        // Arrange
        await using var db = await GetDbContextAsync();
        var user = await TestUserFactory.GetOrCreateTestUserAsync(db);

        var createDto = new ExchangeAccountCreateDto
        {
            UserId = user.Id,
            AccountName = "Binance Test",
            ExchangeName = ExchangeName.Binance,
            ApiKey = "key",
            ApiSecret = "secret"
        };

        var createAccResponse = await _client.PostAsJsonAsync(Routes.ExchangeAccounts, createDto);
        // Debug
        if (!createAccResponse.IsSuccessStatusCode)
        {
            var error = await createAccResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Response failed: " + error);
        }

        createAccResponse.EnsureSuccessStatusCode();

        var createdAcc = await createAccResponse.Content.ReadFromJsonAsync<ExchangeAccountDto>();
        createdAcc.Should().NotBeNull();
        var exchangeAccountId = createdAcc!.Id;

        // Add fake assets
        var newAssets = new List<AssetCreateDto>
        {
            new AssetCreateDto { Ticker = "BTC", Balance = 1.0m, ExchangeAccountId = exchangeAccountId },
            new AssetCreateDto { Ticker = "ETH", Balance = 2.5m, ExchangeAccountId = exchangeAccountId }
        };
        foreach (var asset in newAssets)
        {
            var addAssetsResponse = await _client.PostAsJsonAsync(Routes.Assets, asset);
            addAssetsResponse.EnsureSuccessStatusCode();
        }

        var getAssetsResponse =
            await _client.GetAsync(Routes.AssetsByAccountSourceId + $"?id={exchangeAccountId}");
        // Debug
        if (!getAssetsResponse.IsSuccessStatusCode)
        {
            var error = await getAssetsResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Response failed: " + error);
        }

        getAssetsResponse.EnsureSuccessStatusCode();
        var addedAssets = await getAssetsResponse.Content.ReadFromJsonAsync<List<AssetDto>>();

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            ExchangeAccountId = exchangeAccountId,
            FromAssetId = addedAssets.First().Id,
            ToAssetId = addedAssets.Last().Id,
            FromAmount = 100,
            ToAmount = 150,
            Type = TransactionType.Deposit,
            Timestamp = DateTime.UtcNow,
        };

        await db.Transactions.AddAsync(transaction);
        await db.SaveChangesAsync();

        // Act: Delete exchange account
        var deleteResponse = await _client.DeleteAsync($"{Routes.ExchangeAccounts}/{exchangeAccountId}");
        deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // Assert: ExchangeAccount should be gone
        var deletedAccount = await db.ExchangeAccounts
            .FirstOrDefaultAsync(x => x.Id == exchangeAccountId);
        deletedAccount.Should().BeNull();

        // Assert: Assets should be deleted
        var linkedAssets = await db.Assets
            .Where(a => a.ExchangeAccountId == exchangeAccountId)
            .ToListAsync();
        linkedAssets.Should().BeEmpty();

        // Assert: Transactions should still exist
        var transactions = await db.Transactions
            .Where(t => t.ExchangeAccountId == exchangeAccountId)
            .ToListAsync();
        transactions.Should().NotBeEmpty(); // Assuming you seeded or added one
    }
}

