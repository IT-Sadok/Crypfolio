using Crypfolio.Application.Common;
using Crypfolio.Application.DTOs;

namespace Crypfolio.Application.Interfaces;

public interface IExchangeAccountService
{
    Task<IEnumerable<ExchangeAccountDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ExchangeAccountDto>?> GetAllAsync(string userId, CancellationToken cancellationToken = default);
    Task<PaginatedResultDto<ExchangeAccountDto>> GetPaginatedAsync(ExchangeAccountQueryParams query, CancellationToken ct);

    Task<ExchangeAccountDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ExchangeAccountDto>  CreateExchangeAccountAsync(ExchangeAccountCreateModel model, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Guid id, ExchangeAccountDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}