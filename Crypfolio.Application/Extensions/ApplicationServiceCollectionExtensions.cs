using Crypfolio.Application.Interfaces;
using Crypfolio.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Crypfolio.Application.Extensions;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IExchangeAccountService, ExchangeAccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ExchangeSyncService>();

        return services;
    }
}