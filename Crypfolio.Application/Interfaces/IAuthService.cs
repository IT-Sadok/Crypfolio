using Crypfolio.Application.DTOs;

namespace Crypfolio.Application.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string[] Errors)> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default);
    Task<(bool Success, string Token, string[] Errors)> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);

}