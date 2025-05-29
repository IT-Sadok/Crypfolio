using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crypfolio.Infrastructure.Persistence;

public class UserDataRepository : IUserDataRepository
{
    private readonly ApplicationDbContext _context;

    public UserDataRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token, bool isTracking = false)
    {
        var query = _context.RefreshTokens.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(Guid userId, string deviceId, bool isTracking = false)
    {
        var query = _context.RefreshTokens.AsQueryable();
        if (!isTracking)
            query = query.AsNoTracking();

        return await query
            .Where(t => t.UserId == userId && t.DeviceId == deviceId && !t.IsRevoked)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken)
    {
        await _context.RefreshTokens.AddAsync(token, cancellationToken);
    }
}

