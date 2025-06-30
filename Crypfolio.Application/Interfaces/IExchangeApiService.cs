using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Application.Interfaces;

public interface IExchangeApiService
{
    ExchangeName ExchangeName { get; }
    Task<List<AssetDto>> GetAvailableAssetsAsync(string apiKey, string secretKey, CancellationToken ct);
}