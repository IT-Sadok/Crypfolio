using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string[] Errors)> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default);
    Task<AuthResultDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
    Task<TokenPairDto> RefreshAccessTokenAsync(string refreshToken, string deviceId, CancellationToken cancellationToken = default);
}