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

    public AssetService(IAssetRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
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
    
    public async Task<AssetDto?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetBySymbolAsync(symbol, cancellationToken);
        return asset?.Adapt<AssetDto>();
    }

    public async Task AddAsync(CreateAssetDto dto, CancellationToken cancellationToken)
    {
        var asset = dto.Adapt<Asset>();
        await _repository.AddAsync(asset, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<Result> UpdateAsync(AssetDto dto, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByIdAsync(dto.Id, cancellationToken);
        if (asset == null)
            Result.Fail("Asset is not found");

        asset.Name = dto.Name;
        asset.Symbol = dto.Symbol;
        asset.Balance = dto.Balance;
        asset.AverageBuyPrice = dto.AverageBuyPrice;
     
        //await _repository.UpdateAsync(asset, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
    
    public async Task<Result> UpdateBySymbolAsync(string symbol, AssetDto dto, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetBySymbolAsync(symbol, cancellationToken);
        if (asset == null)
            Result.Fail("Asset is not found");

        asset.Name = dto.Name;
        asset.Symbol = dto.Symbol;
        asset.Balance = dto.Balance;
        asset.AverageBuyPrice = dto.AverageBuyPrice;
        
        //await _repository.UpdateAsync(asset, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }

    public async Task DeleteAsync(string symbol, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(symbol, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}