using Crypfolio.Application.Common;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;
using Mapster;

namespace Crypfolio.Application.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public WalletService(IUnitOfWork unitOfWork)
    {
        _repository = unitOfWork.Wallets;        
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<WalletDto>?> GetAllAsync(string userId, CancellationToken cancellationToken = default)
    { 
        var wallets = await _repository.GetAllAsync(userId, cancellationToken);
        return wallets?.Adapt<IEnumerable<WalletDto>>();
    }

    public async Task<WalletDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var wallet = await _repository.GetByIdAsync(id, cancellationToken);
        return wallet?.Adapt<WalletDto>();
    }

    public async Task AddAsync(WalletDto walletDto, CancellationToken cancellationToken = default)
    {
        var wallet = walletDto.Adapt<Wallet>();
        await _repository.AddAsync(wallet, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<Result> UpdateAsync(Guid id, WalletDto walletDto, CancellationToken cancellationToken = default)
    {
        var wallet = await _repository.GetByIdAsync(id, cancellationToken);
        if (wallet == null)
            return Result.Fail("Asset is not found");
        
        wallet.AccountName = walletDto.Name;
        wallet.WalletType = walletDto.WalletType;
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
