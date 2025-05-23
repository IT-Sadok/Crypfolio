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
            IAuthService authService) =>
        {
            var (success, token, errors) = await authService.LoginAsync(dto);
            if (!success)
                return Results.BadRequest(new { errors });

            return Results.Ok(new { token });
        });

        return endpoints;
    }
}