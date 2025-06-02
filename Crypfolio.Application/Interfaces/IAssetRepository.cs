using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IAssetRepository
{
    Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken, bool isTracking = false);
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool isTracking = false);
    Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken, bool isTracking = false);
    Task AddAsync(Asset asset, CancellationToken cancellationToken);
    Task DeleteAsync(string symbol, CancellationToken cancellationToken);
}
