using Crypfolio.Application.Common;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;
using Mapster;

namespace Crypfolio.Application.Services;

// CRUD, validation, dto transformation
public class ExchangeAccountService : IExchangeAccountService
{
    private readonly IExchangeAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ExchangeAccountService(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.ExchangeAccounts;
        _unitOfWork = unitOfWork;
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

    public async Task<ExchangeAccountDto> CreateExchangeAccountAsync(ExchangeAccountCreateDto dto, CancellationToken cancellationToken = default)
    {
        var entity = new ExchangeAccount
        {
            UserId = dto.UserId,
            AccountName = dto.AccountName,
            ExchangeName = dto.ExchangeName,
            ApiKeyEncrypted = dto.ApiKey,
            ApiSecretEncrypted = dto.ApiSecret,
            ApiPassphrase = dto.ApiPassphrase,
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
        await _repository.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
