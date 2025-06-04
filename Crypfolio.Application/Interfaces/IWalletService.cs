using Crypfolio.Application.Common;
using Crypfolio.Application.DTOs;

namespace Crypfolio.Application.Interfaces;

public interface IWalletService
{
    Task<IEnumerable<WalletDto>?> GetAllAsync(string userId, CancellationToken cancellationToken);
    Task<WalletDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task AddAsync(WalletDto dto, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(Guid id, WalletDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}