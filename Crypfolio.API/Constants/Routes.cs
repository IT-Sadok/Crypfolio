namespace Crypfolio.API.Constants;

public class Routes
{
    public const string Assets = "/api/assets";
    public const string AssetsBySymbol = "/api/assets/{symbol}";
    public const string AssetsById = "/api/assets/{id}";
    public const string Login = "/api/auth/login";
    public const string Register = "/api/auth/register";
    public const string Refresh = "/api/auth/refresh";
}