using Crypfolio.Application.Common;
using Crypfolio.Application.DTOs;

namespace Crypfolio.Application.Interfaces;

public interface IAssetService
{
    public Task<IEnumerable<AssetDto>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(CreateAssetDto dto, CancellationToken cancellationToken);
    Task<AssetDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AssetDto?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(AssetDto dto, CancellationToken cancellationToken);    
    Task<Result> UpdateBySymbolAsync(string symbol, AssetDto dto, CancellationToken cancellationToken);
    public Task DeleteAsync(string symbol, CancellationToken cancellationToken);
}