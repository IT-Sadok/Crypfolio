using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string[] Errors)> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default);
    Task<(bool Success, string AccessToken, string RefreshToken, string[] Errors)> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);
    Task<(string? AccessToken, string? RefreshToken)> RefreshAccessTokenAsync(string refreshToken, string deviceId, CancellationToken cancellationToken);
}