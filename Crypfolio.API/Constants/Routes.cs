namespace Crypfolio.API.Constants;

public class Routes
{
    public const string Assets = "/api/assets";
    public const string AssetsBySymbol = "/api/assets/by-symbol/{symbol}";
    public const string AssetsById = "/api/assets/by-id/{id}";
    public const string Login = "/api/auth/login";
    public const string Register = "/api/auth/register";
}