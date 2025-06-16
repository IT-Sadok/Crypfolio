using Crypfolio.Application.DTOs;

namespace Crypfolio.Application.Interfaces;

public interface IBinanceApiService
{
    Task<List<AssetBalanceDto>> GetAccountBalancesAsync(string apiKey, string secretKey, CancellationToken cancellationToken);
}
