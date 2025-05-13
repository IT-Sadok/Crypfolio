using Crypfolio.Application.Common;
using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;
using Crypfolio.Application.Interfaces;
using Mapster;

namespace Crypfolio.Application.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository _repository;

    public AssetService(IAssetRepository repository)
    {
        _repository = repository;
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
    }
    
    public async Task<Result> UpdateAsync(AssetDto dto, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetByIdAsync(dto.Id, cancellationToken);
        if (asset == null)
            Result.Fail("Asset is not found");

        dto.Adapt(asset); 
        await _repository.UpdateAsync(asset, cancellationToken);
        return Result.Success();
    }
    
    public async Task<Result> UpdateBySymbolAsync(string symbol, AssetDto dto, CancellationToken cancellationToken)
    {
        var asset = await _repository.GetBySymbolAsync(symbol, cancellationToken);
        if (asset == null)
            Result.Fail("Asset is not found");

        dto.Adapt(asset); 
        await _repository.UpdateAsync(asset, cancellationToken);
        return Result.Success();
    }

    public async Task DeleteAsync(string symbol, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(symbol, cancellationToken);
    }
}