using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IAssetRepository
{
    Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken, bool isTracking = false);
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool isTracking = false);
    Task<Asset?> GetByTickerAsync(string ticker, CancellationToken cancellationToken, bool isTracking = false);
    Task<Asset?> GetByNameAndAccountSourceIdAsync(string name, Guid? accountSourceId, CancellationToken cancellationToken);
    Task AddAsync(Asset asset, CancellationToken cancellationToken);
    Task DeleteAsync(string ticker, CancellationToken cancellationToken);
    Task UpsertAssetAsync(Asset asset, CancellationToken cancellationToken);
}