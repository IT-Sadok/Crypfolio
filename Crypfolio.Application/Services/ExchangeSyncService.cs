using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Services;

public class ExchangeSyncService
{
    private readonly IBinanceApiService _binanceApiService;
    private readonly IAssetRepository _assetRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExchangeSyncService(IBinanceApiService binanceApiService, IUnitOfWork unitOfWork)
    {
        _binanceApiService = binanceApiService;
        _unitOfWork = unitOfWork;
        _assetRepository = unitOfWork.Assets;
    }

    public async Task SyncBinanceAccountAsync(ExchangeAccount account, CancellationToken cancellationToken)
    {
        var assets = await _binanceApiService.GetAvailableAssetsAsync(
            account.ApiKeyEncrypted, account.ApiSecretEncrypted, cancellationToken);

        foreach (var binanceAsset in assets)
        {
            // Update assets in DB
            var asset = new Asset
            {
                Ticker = binanceAsset.Ticker,
                Balance = binanceAsset.FreeBalance + binanceAsset.LockedBalance,
                ExchangeAccountId = account.Id,
                RetrievedAt = DateTime.UtcNow,
            };
            await _assetRepository.UpsertAssetAsync(asset, cancellationToken);
        }
    }
}
