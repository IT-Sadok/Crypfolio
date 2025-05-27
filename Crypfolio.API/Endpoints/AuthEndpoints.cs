using Crypfolio.API.Constants;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;

namespace Crypfolio.Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(Routes.Register, async (
            RegisterDto dto,
            IAuthService authService) =>
        {
            var (success, errors) = await authService.RegisterAsync(dto);
            if (!success)
                return Results.BadRequest(new { errors });

            return Results.Ok(new { message = "Registration successful" });
        });
        
        endpoints.MapPost(Routes.Login, async (
            LoginDto dto,
            IAuthService authService,
            HttpResponse response) =>
        {
            var (success, accessToken, refreshToken, errors) = await authService.LoginAsync(dto);
            if (!success)
                return Results.BadRequest(new { errors });

            response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(30),
                SameSite = SameSiteMode.Strict
            });

            return Results.Ok(new { accessToken });
        });

        return endpoints;
    }
}