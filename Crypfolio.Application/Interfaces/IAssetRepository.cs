using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IAssetRepository
{
    Task<IEnumerable<Asset>> GetAllAsync();
    Task<Asset?> GetByIdAsync(Guid id);
    Task<Asset?> GetBySymbolAsync(string symbol);
    Task AddAsync(Asset asset);
    Task UpdateAsync(Asset asset);
    Task DeleteAsync(string symbol);
}
