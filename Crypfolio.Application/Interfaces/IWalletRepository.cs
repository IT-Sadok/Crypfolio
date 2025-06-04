using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IWalletRepository
{
    Task<IEnumerable<Wallet>> GetAllAsync(string userId, CancellationToken cancellationToken = default, bool isTracking = false);
    Task<Wallet?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool isTracking = false);
    Task AddAsync(Wallet wallet, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}