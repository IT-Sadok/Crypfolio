using Crypfolio.Application.Common;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Crypfolio.Application.Services;

public class ExchangeAccountService : IExchangeAccountService
{
    private readonly IExchangeAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ExchangeAccountService> _logger;
    
    public ExchangeAccountService(IUnitOfWork unitOfWork, ILogger<ExchangeAccountService> logger)
    {
        _repository = unitOfWork.ExchangeAccounts;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<ExchangeAccountDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var accounts = await _repository.GetAllAsync(cancellationToken);
        return accounts.Adapt<IEnumerable<ExchangeAccountDto>>();
    }
    
    public async Task<IEnumerable<ExchangeAccountDto>?> GetAllAsync(string userId, CancellationToken cancellationToken = default)
    {
        var accounts = await _repository.GetAllAsync(userId, cancellationToken);
        return accounts?.Adapt<IEnumerable<ExchangeAccountDto>>();
    }

    public async Task<ExchangeAccountDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var account = await _repository.GetByIdAsync(id, cancellationToken);
        return account?.Adapt<ExchangeAccountDto>();
    }

    public async Task<ExchangeAccountDto> CreateExchangeAccountAsync(ExchangeAccountCreateModel model, CancellationToken cancellationToken = default)
    {
        var entity = new ExchangeAccount
        {
            UserId = model.UserId,
            //User = new() { Id = dto.UserId },
            AccountName = model.AccountName,
            ExchangeName = model.ExchangeName,
            ApiKeyEncrypted = model.ApiKey,
            ApiSecretEncrypted = model.ApiSecret,
            ApiPassphrase = model.ApiPassphrase,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ExchangeAccountDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            AccountName = entity.AccountName,
            ExchangeName = entity.ExchangeName,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public async Task<Result> UpdateAsync(Guid id, ExchangeAccountDto dto, CancellationToken cancellationToken = default)
    {
        var account = await _repository.GetByIdAsync(id, cancellationToken);
        if (account == null)
            return Result.Fail("Exchange account not found");

        account.ExchangeName = dto.ExchangeName;
        account.ApiKeyEncrypted = dto.ApiKeyEncrypted;
        account.ApiSecretEncrypted = dto.ApiSecretEncrypted;
        account.ApiPassphrase = dto.ApiPassphrase;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _repository.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete entity with ID {Id}", id);
            throw;
        }
    }
    
    public async Task<PaginatedResultDto<ExchangeAccountDto>> GetPaginatedAsync(ExchangeAccountQueryParams query, CancellationToken ct)
    {
        var accountsQuery = _unitOfWork.ExchangeAccounts.GetQueryable();
    
        if (query.UserId.HasValue)
            accountsQuery = accountsQuery.Where(x => x.UserId == query.UserId.Value.ToString());

        var totalCount = await accountsQuery.CountAsync(ct);
        var items = await accountsQuery
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(x => new ExchangeAccountDto
            {
                Id = x.Id,
                AccountName = x.AccountName,
                ExchangeName = x.ExchangeName,
                // map other properties
            })
            .ToListAsync(ct);

        return new PaginatedResultDto<ExchangeAccountDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }
}
