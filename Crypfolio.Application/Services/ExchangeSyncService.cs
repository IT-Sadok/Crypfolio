using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;
using Crypfolio.Domain.Enums;
using Crypfolio.Infrastructure.Services;

namespace Crypfolio.Application.Services;

public class ExchangeSyncService : IExchangeSyncService
{
    private readonly Dictionary<ExchangeName, IExchangeApiService> _apiServices;
    private readonly IUnitOfWork _unitOfWork;

    public ExchangeSyncService(IEnumerable<IExchangeApiService> services, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        // _apiServices = services.ToDictionary(
        //     s => s switch
        //     {
        //         BinanceApiService => ExchangeName.Binance,
        //         // Add others as needed
        //         _ => throw new NotSupportedException($"Unknown service type: {s.GetType().Name}")
        //     });
        
        _apiServices = services.ToDictionary(s => s.ExchangeName);
    }

    public async Task SyncAccountAsync(ExchangeAccount account, CancellationToken cancellationToken)
    {
        if (!_apiServices.TryGetValue(account.ExchangeName, out var service))
            throw new NotSupportedException($"Exchange '{account.ExchangeName}' is not supported.");

        var assets = await service.GetAvailableAssetsAsync(
            account.ApiKeyEncrypted!, account.ApiSecretEncrypted!, cancellationToken);

        foreach (var asset in assets)
        {
            var entity = new Asset
            {
                Ticker = asset.Ticker,
                FreeBalance = asset.FreeBalance,
                LockedBalance = asset.LockedBalance,
                ExchangeAccountId = account.Id,
                UpdatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.Assets.UpsertAssetAsync(entity, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}