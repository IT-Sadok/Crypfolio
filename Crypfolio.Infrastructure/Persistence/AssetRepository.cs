using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crypfolio.Infrastructure.Persistence;

public class AssetRepository : IAssetRepository
{
    private readonly ApplicationDbContext _context;

    public AssetRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Asset>> GetAllAsync(CancellationToken cancellationToken = default, bool isTracking = false)
    {
        var query = _context.Assets.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool isTracking = false)
    {
        var query = _context.Assets.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Asset?> GetBySymbolAsync(string symbol, CancellationToken cancellationToken = default, bool isTracking = false)
    {
        var query = _context.Assets.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(a => a.Symbol == symbol.ToLowerInvariant(), cancellationToken);
    }

    public async Task AddAsync(Asset asset, CancellationToken cancellationToken = default)
    {
        await _context.Assets.AddAsync(asset, cancellationToken);
    }

    public async Task DeleteAsync(string symbol, CancellationToken cancellationToken = default)
    {
        var asset = await _context.Assets.FindAsync(new object[] { symbol }, cancellationToken);
        if (asset != null)
        {
            _context.Assets.Remove(asset);
        }
    }
}
