using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IAssetRepository
{
    Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken);
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken);
    Task AddAsync(Asset asset, CancellationToken cancellationToken);
    Task UpdateAsync(Asset asset, CancellationToken cancellationToken);
    Task DeleteAsync(string symbol, CancellationToken cancellationToken);
}
