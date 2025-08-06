using Crypfolio.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Crypfolio.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    
    public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;

        ExchangeAccounts = new ExchangeAccountRepository(_context, null);
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save changes");
            throw;
        }
    }
}
