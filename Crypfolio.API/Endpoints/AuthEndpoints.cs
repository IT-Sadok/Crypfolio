using Crypfolio.API.Constants;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crypfolio.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(Routes.Register, async (
            RegisterDto dto,
            [FromServices] IAuthService authService) =>
        {
            var (success, errors) = await authService.RegisterAsync(dto);
            if (!success)
                return Results.BadRequest(new { errors });

            return Results.Ok(new { message = "Registration successful" });
        });
        
        endpoints.MapPost(Routes.Login, async (
            LoginDto dto,
            [FromServices] IAuthService authService,
            HttpResponse response) =>
        {
            var result = await authService.LoginAsync(dto);
            if (!result.Success)
                return Results.BadRequest(new { result.Errors });

            response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(30),
                SameSite = SameSiteMode.Strict
            });
            
            response.Cookies.Append("deviceId", dto.DeviceId, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(30),
                SameSite = SameSiteMode.Strict
            });

            return Results.Ok(new { result.AccessToken });
        });

        endpoints.MapPost(Routes.Refresh, async (
            HttpRequest request,
            [FromServices] IAuthService authService) =>
        {
            var refreshToken = request.Cookies["refreshToken"];
            var deviceId = request.Cookies["deviceId"];
            if (string.IsNullOrEmpty(refreshToken))
                return Results.Unauthorized();

            var newAccessToken = await authService.RefreshAccessTokenAsync(refreshToken, deviceId);
            if (newAccessToken is null)
                return Results.Unauthorized();

            return Results.Ok(new { accessToken = newAccessToken });
        });
    }
}