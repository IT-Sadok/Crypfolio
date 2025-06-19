using Crypfolio.Application.Services;
using Crypfolio.Domain.Entities;
using Crypfolio.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Crypfolio.IntegrationTests.Tests;

public class ExchangeSyncServiceTests : IntegrationTestBase
{
    public ExchangeSyncServiceTests(ILogger<IntegrationTestBase> logger) : base(logger)
    {
    }

    [Fact]
    public async Task SyncBinanceAccountAsync_AddsOrUpdatesAssets()
    {
        using var scope = ScopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<ExchangeSyncService>();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var account = new ExchangeAccount
        {
            AccountName = "Test Binance Account",
            UserId = "test-user-id",
            ApiKeyEncrypted = "dummy",
            ApiSecretEncrypted = "dummy"
        };

        db.ExchangeAccounts.Add(account);
        await db.SaveChangesAsync();

        await service.SyncBinanceAccountAsync(account, CancellationToken.None);

        var assets = db.Assets.Where(a => a.ExchangeAccountId == account.Id).ToList();

        Assert.Equal(2, assets.Count);
        Assert.Contains(assets, a => a.Name == "BTC" && a.Balance == 0.30m);
        Assert.Contains(assets, a => a.Name == "ETH" && a.Balance == 2.0m);
    }
}
