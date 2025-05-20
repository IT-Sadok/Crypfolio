using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;

namespace Crypfolio.Infrastructure.Persistence;

public class InMemoryAssetRepository : IAssetRepository
{
    private readonly List<Asset> _assets = new();

    public Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken) 
        => Task.FromResult(_assets.AsEnumerable());

    public Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var asset = _assets.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(asset);
    }

    public Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken)
    {
        var asset = _assets.FirstOrDefault(a => a.Symbol.Equals(symbol.ToLowerInvariant()));
        return Task.FromResult(asset);
    }

    public Task AddAsync(Asset asset, CancellationToken cancellationToken)
    {
        asset.Id = Guid.NewGuid();
        _assets.Add(asset);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Asset asset, CancellationToken cancellationToken)
    {
        var index = _assets.FindIndex(a => a.Id == asset.Id);
        if (index != -1)
        {
            _assets[index] = asset;
        }
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string symbol, CancellationToken cancellationToken)
    {
        var asset = _assets.FirstOrDefault(a => a.Symbol.Equals(symbol.ToLowerInvariant()));
        if (asset != null)
        {
            _assets.Remove(asset);
        }

        return Task.CompletedTask;
    }
}
