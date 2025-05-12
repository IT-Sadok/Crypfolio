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

    public async Task<IEnumerable<AssetDto>> GetAllAsync()
    {
        var assets = await _repository.GetAllAsync();
        return assets.Adapt<IEnumerable<AssetDto>>();
    }

    public async Task<AssetDto?> GetByIdAsync(Guid id)
    {
        var asset = await _repository.GetByIdAsync(id);
        return asset?.Adapt<AssetDto>();
    }
    
    public async Task<AssetDto?> GetBySymbolAsync(string symbol)
    {
        var asset = await _repository.GetBySymbolAsync(symbol);
        return asset?.Adapt<AssetDto>();
    }

    public async Task AddAsync(CreateAssetDto dto)
    {
        var asset = dto.Adapt<Asset>();
        await _repository.AddAsync(asset);
    }
    
    public async Task UpdateAsync(AssetDto dto)
    {
        var asset = await _repository.GetByIdAsync(dto.Id);
        if (asset == null)
            throw new Exception("Asset not found");

        dto.Adapt(asset); 
        await _repository.UpdateAsync(asset);
    }

    public async Task DeleteAsync(string symbol)
    {
        await _repository.DeleteAsync(symbol);
    }
}