using Crypfolio.Domain.Entities;
using Crypfolio.Application.DTOs;

namespace Crypfolio.Application.Interfaces;

public interface IAssetService
{
    public Task<IEnumerable<AssetDto>> GetAllAsync();
    public Task AddAsync(CreateAssetDto dto);
    Task<AssetDto?> GetByIdAsync(Guid id);
    Task<AssetDto?> GetBySymbolAsync(string symbol);
    Task UpdateAsync(AssetDto dto);
    public Task DeleteAsync(string symbol);
}