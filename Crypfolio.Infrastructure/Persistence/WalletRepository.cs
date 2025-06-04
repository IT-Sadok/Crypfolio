using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crypfolio.Infrastructure.Persistence;

public class WalletRepository : IWalletRepository
{
    private readonly ApplicationDbContext _context;

    public WalletRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Wallet>> GetAllAsync(string userId, CancellationToken cancellationToken = default, bool isTracking = false)
    {
        var query = _context.Wallets.Where(w => w.UserId == userId).AsQueryable();;
        if (!isTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Wallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool isTracking = false)
    {
        var query = _context.Wallets.Where(w => w.Id == id).AsQueryable();;
        if (!isTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(Wallet wallet, CancellationToken cancellationToken = default)
    {
        await _context.Wallets.AddAsync(wallet, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var wallet = await _context.Wallets.FindAsync(new object[] { id }, cancellationToken);
        if (wallet is not null)
            _context.Wallets.Remove(wallet);
    }
}