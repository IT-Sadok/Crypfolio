using Crypfolio.Application.Interfaces;

namespace Crypfolio.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;

        ExchangeAccounts = new ExchangeAccountRepository(_context);
        Wallets = new WalletRepository(_context);
        Assets = new AssetRepository(_context);
        UserData = new UserDataRepository(_context);
    }

    public IExchangeAccountRepository ExchangeAccounts { get; }
    public IWalletRepository Wallets { get; }
    public IAssetRepository Assets { get; }
    public IUserDataRepository UserData { get; }
    

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
