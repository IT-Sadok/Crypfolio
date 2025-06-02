using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IUserDataRepository
{
    Task<RefreshToken?> GetRefreshTokenAsync(string token, bool isTracking = false);
    Task<RefreshToken?> GetRefreshTokenAsync(Guid userId, string deviceId, bool isTracking = false);
    Task AddAsync(RefreshToken token, CancellationToken cancellationToken);
}