using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IUserDataRepository
{
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task<RefreshToken?> GetRefreshTokenAsync(string userId, string deviceId);
    Task UpdateAsync(RefreshToken token, CancellationToken cancellationToken);
    Task AddAsync(RefreshToken token, CancellationToken cancellationToken);
    
    // Add future user-data related methods here
}