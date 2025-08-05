using Crypfolio.Application.Common;
using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;
using Crypfolio.Application.Interfaces;
using Mapster;

namespace Crypfolio.Application.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public AssetService(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.Assets;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AssetDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var assets = await _repository.GetAllAsync(cancellationToken);
        return assets.Adapt<IEnumerable<AssetDto>>();
    }

    public async Task<AssetDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByIdAsync(id, cancellationToken);
        return asset?.Adapt<AssetDto>();
    }
    
    public async Task<List<AssetDto>?> GetByAccountSourceIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var assets = await _repository.GetByAccountSourceIdAsync(id, cancellationToken);
        return assets?.Adapt<List<AssetDto>>();
    }
    
    public async Task<AssetDto?> GetByTickerAsync(string ticker, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByTickerAsync(ticker, cancellationToken);
        return asset?.Adapt<AssetDto>();
    }

    public async Task AddAsync(AssetCreateDto dto, CancellationToken cancellationToken)
    {
        var asset = dto.Adapt<Asset>();
        await _repository.AddAsync(asset, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Result> UpdateAsync(AssetDto dto, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByIdAsync(dto.Id, cancellationToken, true);
        if (asset == null)
            return Result.Fail("Asset is not found");

        asset.Name = dto.Name;
        asset.Ticker = dto.Ticker;
        asset.FreeBalance = dto.FreeBalance;
        asset.LockedBalance = dto.LockedBalance;
        asset.AverageBuyPrice = dto.AverageBuyPrice;
     
        //await _repository.UpdateAsync(asset, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
    
    public async Task<Result> UpdateByTickerAsync(string ticker, AssetDto dto, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByTickerAsync(ticker, cancellationToken, true);
        if (asset == null)
            return Result.Fail("Asset is not found");

        asset.Name = dto.Name;
        asset.Ticker = dto.Ticker;
        asset.FreeBalance = dto.FreeBalance;
        asset.LockedBalance = dto.LockedBalance;
        asset.AverageBuyPrice = dto.AverageBuyPrice;
        
        //await _repository.UpdateAsync(asset, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }

    public async Task DeleteAsync(string ticker, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(ticker, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}