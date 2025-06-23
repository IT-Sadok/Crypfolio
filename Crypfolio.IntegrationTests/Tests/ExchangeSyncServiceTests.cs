using Crypfolio.Application.Services;
using Crypfolio.Domain.Entities;
using Crypfolio.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Crypfolio.IntegrationTests.Tests;

public class ExchangeSyncServiceTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly IServiceScopeFactory _scopeFactory;

    public ExchangeSyncServiceTests(CustomWebApplicationFactory factory)
    {
        // Obtain the IServiceScopeFactory from the factory's service provider
        _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    [Fact]
    public async Task SyncBinanceAccountAsync_AddsOrUpdatesAssets()
    {
        // Arrange: create a scope to access DbContext and services
        await using var scope = _scopeFactory.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var service = scope.ServiceProvider.GetRequiredService<ExchangeSyncService>();

        var account = new ExchangeAccount
        {
            AccountName = "Test Binance Account",
            UserId = "test-user-id",
            ApiKeyEncrypted = "dummy",
            ApiSecretEncrypted = "dummy"
        };

        db.ExchangeAccounts.Add(account);
        await db.SaveChangesAsync();

        // Act
        await service.SyncBinanceAccountAsync(account, CancellationToken.None);

        // Assert: two assets were upserted
        var assets = db.Assets.Where(a => a.ExchangeAccountId == account.Id).ToList();
        Assert.Equal(2, assets.Count);
        Assert.Contains(assets, a => a.Name == "BTC" && a.Balance == 0.30m);
        Assert.Contains(assets, a => a.Name == "ETH" && a.Balance == 2.0m);
    }
}

