using Crypfolio.API.Constants;
using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Crypfolio.Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost(Routes.Register, async (
            RegisterDto dto,
            UserManager<ApplicationUser> userManager) =>
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return Results.BadRequest(errors);
            }

            return Results.Ok("User registered successfully.");
        });
        
        endpoints.MapPost(Routes.Login, async (
            LoginRequestDto request,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) =>
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Results.BadRequest("Invalid email or password");

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return Results.BadRequest("Invalid email or password");

            // replace this with JWT or cookie auth?
            return Results.Ok("Login successful");
        });

        return endpoints;
    }
}