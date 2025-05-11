using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;
using Crypfolio.Application.Interfaces;

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
        return assets.Select(asset => new AssetDto
        {
            Id = asset.Id,
            Symbol = asset.Symbol,
            Name = asset.Name,
            Balance = asset.Balance,
            AverageBuyPrice = asset.AverageBuyPrice,
            UsdValue = asset.UsdValue,
            WalletId = asset.WalletId,
            AccountId = asset.AccountId,
            SourceType = asset.SourceType,
            RetrievedAt = asset.RetrievedAt
        }).ToList();
    }

    public async Task<AssetDto?> GetByIdAsync(Guid id)
    {
        var asset = await _repository.GetByIdAsync(id);
        if (asset == null) return null;

        return new AssetDto
        {
            Id = asset.Id,
            Symbol = asset.Symbol,
            Name = asset.Name,
            Balance = asset.Balance,
            AverageBuyPrice = asset.AverageBuyPrice,
            UsdValue = asset.UsdValue,
            WalletId = asset.WalletId,
            AccountId = asset.AccountId,
            SourceType = asset.SourceType,
            RetrievedAt = asset.RetrievedAt
        };
    }
    
    public async Task<AssetDto?> GetBySymbolAsync(string symbol)
    {
        var asset = await _repository.GetBySymbolAsync(symbol);
        if (asset == null)
            return null;

        return new AssetDto
        {
            Id = asset.Id,
            Symbol = asset.Symbol,
            Name = asset.Name,
            Balance = asset.Balance,
            AverageBuyPrice = asset.AverageBuyPrice,
            UsdValue = asset.UsdValue,
            WalletId = asset.WalletId,
            AccountId = asset.AccountId,
            SourceType = asset.SourceType,
            RetrievedAt = asset.RetrievedAt
        };
    }

    public async Task AddAsync(CreateAssetDto dto)
    {
        var asset = new Asset
        {
            Symbol = dto.Symbol,
            Name = dto.Name,
            Balance = dto.Balance,
            AverageBuyPrice = dto.AverageBuyPrice,
            WalletId = dto.WalletId,
            AccountId = dto.AccountId,
            SourceType = dto.SourceType
        };

        await _repository.AddAsync(asset);
    }
    
    public async Task UpdateAsync(AssetDto dto)
    {
        var asset = await _repository.GetByIdAsync(dto.Id);
        if (asset == null)
            throw new Exception("Asset not found");

        asset.Name = dto.Name;
        asset.Balance = dto.Balance;
        asset.AverageBuyPrice = dto.AverageBuyPrice;

        await _repository.UpdateAsync(asset);
    }

    public async Task DeleteAsync(string symbol)
    {
        await _repository.DeleteAsync(symbol);
    }
}