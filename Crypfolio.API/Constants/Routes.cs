namespace Crypfolio.API.Constants;

public class Routes
{
    public const string Assets = "/api/assets";
    public const string AssetsByTicker = "/api/assets/{symbol}";
    public const string AssetsById = "/api/assets/{id}";
    public const string AssetsByAccountSourceId = "/api/assets/sync";
    
    public const string Login = "/api/auth/login";
    public const string Register = "/api/auth/register";
    public const string Refresh = "/api/auth/refresh";
    
    public const string Wallets = "/api/wallets";
    public const string WalletsById = "/api/wallets/detail/{id:guid}"; 
    public const string WalletsByUserId = "/api/wallets/{userId:guid}"; 

    public const string ExchangeAccounts = "/api/exchange-accounts";
    public const string ExchangeAccountsSync = "/api/exchange-accounts/sync";
    public const string ExchangeAccountsById = "/api/exchange-accounts/{id:guid}";
    public const string ExchangeAccountsByUserId = "/api/exchange-accounts/user"; //{userId:guid}
    public const string ExchangeAccountsPaginated = "/api/exchange-accounts/paginated";
}