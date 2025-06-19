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

    public async Task<Asset?> GetByTickerAsync(string ticker, CancellationToken cancellationToken = default, bool isTracking = false)
    {
        var query = _context.Assets.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(a => a.Ticker == ticker.ToLowerInvariant(), cancellationToken);
    }
    
    public async Task<Asset?> GetByNameAndAccountSourceIdAsync(string name, Guid? accoutSourceId,
        CancellationToken cancellationToken)
    {
        return await _context.Assets.FirstOrDefaultAsync(a => 
            a.Name == name && 
            (a.ExchangeAccountId == accoutSourceId || a.WalletId == accoutSourceId), 
            cancellationToken);
    }
    
    public async Task UpsertAssetAsync(Asset asset, CancellationToken cancellationToken)
    {
        var existing = await GetByNameAndAccountSourceIdAsync(
            asset.Name, 
            asset.ExchangeAccountId ?? asset.WalletId, 
            cancellationToken);

        if (existing is null)
        {
            await _context.Assets.AddAsync(asset, cancellationToken);
        }
        else
        {
            existing.Balance = asset.Balance;
            existing.RetrievedAt = asset.RetrievedAt;
            _context.Assets.Update(existing);
        }
    }

    public async Task AddAsync(Asset asset, CancellationToken cancellationToken = default)
    {
        await _context.Assets.AddAsync(asset, cancellationToken);
    }

    public async Task DeleteAsync(string ticker, CancellationToken cancellationToken = default)
    {
        var asset = await _context.Assets.FirstOrDefaultAsync(c => c.Ticker == ticker, cancellationToken: cancellationToken);
        if (asset != null)
        {
            _context.Assets.Remove(asset);
        }
    }
}
