using Crypfolio.Application.Common;
using Crypfolio.Application.DTOs;

namespace Crypfolio.Application.Interfaces;

public interface IAssetService
{
    public Task<IEnumerable<AssetDto>> GetAllAsync(CancellationToken cancellationToken);
    public Task AddAsync(AssetCreateDto dto, CancellationToken cancellationToken);
    Task<AssetDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<AssetDto?> GetByTickerAsync(string ticker, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(AssetDto dto, CancellationToken cancellationToken);    
    Task<Result> UpdateByTickerAsync(string ticker, AssetDto dto, CancellationToken cancellationToken);
    public Task DeleteAsync(string ticker, CancellationToken cancellationToken);
}