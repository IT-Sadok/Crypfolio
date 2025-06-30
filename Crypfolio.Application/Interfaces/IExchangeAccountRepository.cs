using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IExchangeAccountRepository
{
    Task<IEnumerable<ExchangeAccount>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ExchangeAccount>> GetAllAsync(string userId, CancellationToken cancellationToken = default, bool isTracking = false);
    Task<ExchangeAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool isTracking = false);
    Task AddAsync(ExchangeAccount account, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}