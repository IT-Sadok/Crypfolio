using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Crypfolio.Application.Services;

public class AuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserDataRepository _userDataRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    
    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserDataRepository userDataRepository,
        ITokenService tokenService,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userDataRepository = userDataRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<(bool Success, string[] Errors)> RegisterAsync(RegisterDto dto,
        CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return (false, result.Errors.Select(e => e.Description).ToArray());

        // optionally: await _userManager.AddToRoleAsync(user, "User");
        
        return (true, Array.Empty<string>());
    }

    public async Task<AuthResultDto> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var response = new AuthResultDto
        {
            Success = false,
            AccessToken = "",
            RefreshToken = "",
            Errors = new[] { "Invalid login attempt." }
        };

        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return response;

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
            return response;

        var roles = await _userManager.GetRolesAsync(user);
        var accessToken = _tokenService.GenerateAccessToken(user, roles);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var existingToken = await _userDataRepository.GetRefreshTokenAsync(Guid.Parse(user.Id), dto.DeviceId);
        if (existingToken != null)
        {
            existingToken.Token = refreshToken;
            existingToken.CreatedAt = DateTime.UtcNow;
            existingToken.ExpiresAt = DateTime.UtcNow.AddDays(30);
            existingToken.IsRevoked = false;
            existingToken.RevokedAt = null;

            await _userDataRepository.UpdateAsync(existingToken, cancellationToken);
        }
        else
        {
            var newToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = refreshToken,
                UserId = Guid.Parse(user.Id),
                DeviceId = dto.DeviceId,
                DeviceName = "", // optionally
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(30),
                IsRevoked = false
            };

            await _userDataRepository.AddAsync(newToken, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        response.Success = true;
        response.AccessToken = accessToken;
        response.RefreshToken = refreshToken;
        response.Errors = Array.Empty<string>();
        return response;
    }

    public async Task<TokenPairDto> RefreshAccessTokenAsync(string refreshToken, string deviceId, CancellationToken cancellationToken = default)
    {
        var response = new TokenPairDto { AccessToken = null, RefreshToken = null };
        var tokenEntity = await _userDataRepository.GetRefreshTokenAsync(refreshToken);
        if (tokenEntity == null || !tokenEntity.IsActive || tokenEntity.DeviceId != deviceId)
            return response;

        var user = await _userManager.FindByIdAsync(tokenEntity.UserId.ToString());
        if (user == null)
            return response;

        var roles = await _userManager.GetRolesAsync(user);
        var newAccessToken = _tokenService.GenerateAccessToken(user, roles);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        tokenEntity.Token = newRefreshToken;
        tokenEntity.CreatedAt = DateTime.UtcNow;
        tokenEntity.ExpiresAt = DateTime.UtcNow.AddDays(30);
        tokenEntity.RevokedAt = null;
        tokenEntity.IsRevoked = false;

        await _userDataRepository.UpdateAsync(tokenEntity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        response.AccessToken = newAccessToken;
        response.RefreshToken = newRefreshToken;
        return response;
    }

}