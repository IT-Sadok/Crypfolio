using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crypfolio.Infrastructure.Persistence;

public class AssetRepository : IAssetRepository
{
    private readonly ApplicationDbContext _context;
    private readonly List<Asset> _assets = new();

    public AssetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Assets.ToListAsync(cancellationToken);
    }

    public async Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Assets.FindAsync( new object[] { id }, cancellationToken);
    }

    public async Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken)
    {
        return await _context.Assets.FindAsync(new object[] { symbol.ToLowerInvariant() }, cancellationToken);
    }

    public async Task AddAsync(Asset asset, CancellationToken cancellationToken)
    {
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Asset asset, CancellationToken cancellationToken)
    {
        _context.Assets.Update(asset);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(string symbol, CancellationToken cancellationToken)
    {
        var asset = await _context.Assets.FindAsync(new object[] { symbol }, cancellationToken);
        if (asset != null)
        {
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync(cancellationToken); 
        }
    }
}
