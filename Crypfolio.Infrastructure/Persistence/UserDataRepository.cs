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

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == token);
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string userId, string deviceId)
    {
        var uid = Guid.Parse(userId);

        return await _context.RefreshTokens
            .Where(t => t.UserId == uid && t.DeviceId == deviceId && !t.IsRevoked)
            .OrderByDescending(t => t.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(RefreshToken token, CancellationToken cancellationToken)
    {
        _context.RefreshTokens.Add(token);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(RefreshToken token, CancellationToken cancellationToken)
    {
        _context.RefreshTokens.Update(token);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

