namespace Crypfolio.Application.Interfaces;

public interface IUnitOfWork
{
    IExchangeAccountRepository ExchangeAccounts { get; }
    IWalletRepository Wallets { get; }
    IAssetRepository Assets { get; }
    IUserDataRepository UserData { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}