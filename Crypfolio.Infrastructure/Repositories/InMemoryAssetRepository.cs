using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;

namespace Crypfolio.Infrastructure.Repositories;

public class InMemoryAssetRepository : IAssetRepository
{
    private readonly List<Asset> _assets = new();

    public Task<IEnumerable<Asset>> GetAllAsync() => Task.FromResult(_assets.AsEnumerable());

    public Task<Asset?> GetByIdAsync(Guid id)
    {
        var asset = _assets.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(asset);
    }

    public Task<Asset?> GetBySymbolAsync(string symbol)
    {
        var asset = _assets.FirstOrDefault(a => a.Symbol.Equals(symbol));
        return Task.FromResult(asset);
    }

    public Task AddAsync(Asset asset)
    {
        asset.Id = Guid.NewGuid();
        _assets.Add(asset);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Asset asset)
    {
        var index = _assets.FindIndex(a => a.Id == asset.Id);
        if (index != -1)
        {
            _assets[index] = asset;
        }

        return Task.CompletedTask;
    }

    public Task DeleteAsync(string symbol)
    {
        var asset = _assets.FirstOrDefault(a => a.Symbol.Equals(symbol));
        if (asset != null)
        {
            _assets.Remove(asset);
        }

        return Task.CompletedTask;
    }
}
