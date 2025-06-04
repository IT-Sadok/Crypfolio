using Crypfolio.API.Constants;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;

namespace Crypfolio.API.Endpoints;

public static class ExchangeAccountEndpoints
{
    public static void MapExchangeAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(Routes.ExchangeAccountsByUserId, async (string userId, IExchangeAccountService service, CancellationToken ct) =>
        {
            var accounts = await service.GetAllAsync(userId, ct);
            return Results.Ok(accounts);
        });

        endpoints.MapGet(Routes.ExchangeAccountsById, async (Guid id, IExchangeAccountService service, CancellationToken ct) =>
        {
            var account = await service.GetByIdAsync(id, ct);
            return account is null ? null : Results.Ok(account);
        });

        endpoints.MapPost(Routes.ExchangeAccounts, async (ExchangeAccountDto dto, IExchangeAccountService service, CancellationToken ct) =>
        {
            await service.AddAsync(dto, ct);
            return Results.Created($"{Routes.ExchangeAccounts}/{dto.Id}", dto);
        });

        endpoints.MapPut(Routes.ExchangeAccountsById, async (Guid id, ExchangeAccountDto dto, IExchangeAccountService service, CancellationToken ct) =>
        {
            var result = await service.UpdateAsync(id, dto, ct);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });

        endpoints.MapDelete(Routes.ExchangeAccountsById, async (Guid id, IExchangeAccountService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });
    }
}