using Crypfolio.Domain.Entities;
using Crypfolio.Application.DTOs;

namespace Crypfolio.Application.Interfaces;

public interface IAssetService
{
    public Task<IEnumerable<AssetDto>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(CreateAssetDto dto, CancellationToken cancellationToken);
    Task<AssetDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AssetDto?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken);
    Task UpdateAsync(AssetDto dto, CancellationToken cancellationToken);
    public Task DeleteAsync(string symbol, CancellationToken cancellationToken);
}