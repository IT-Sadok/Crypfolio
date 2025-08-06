using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Crypfolio.Infrastructure.Persistence;

public class ExchangeAccountRepository : IExchangeAccountRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ExchangeAccountRepository> _logger;

    public ExchangeAccountRepository(ApplicationDbContext context, ILogger<ExchangeAccountRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ExchangeAccount>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.ExchangeAccounts.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ExchangeAccount>> GetAllAsync(string userId, CancellationToken cancellationToken = default, bool isTracking = false)
    {
        var query = _context.ExchangeAccounts.Where(a => a.UserId == userId);
        if (!isTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<ExchangeAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool isTracking = false)
    {
        var query = _context.ExchangeAccounts.Where(a => a.Id == id);
        if (!isTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(ExchangeAccount account, CancellationToken cancellationToken = default)
    {
        await _context.ExchangeAccounts.AddAsync(account, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await _context.ExchangeAccounts.FindAsync(new object[] { id }, cancellationToken);
        try
        {
            if (account is not null)
                _context.ExchangeAccounts.Remove(account);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete entity with ID {Id}", id);
            throw;
        }
    }
    
    public IQueryable<ExchangeAccount> GetQueryable(bool includeRelated = false)
    {
        var query = _context.ExchangeAccounts.AsQueryable();

        if (includeRelated)
        {
            query = query.Include(x => x.Assets); // or other navigation properties
        }

        return query;
    }
}