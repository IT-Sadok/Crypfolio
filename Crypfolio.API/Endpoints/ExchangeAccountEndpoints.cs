using Crypfolio.API.Constants;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crypfolio.API.Endpoints;

public static class ExchangeAccountEndpoints
{
    public static void MapExchangeAccountEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(Routes.ExchangeAccounts, async ([FromServices] IExchangeAccountService service, CancellationToken ct) =>
        {
            // Optional: expose all exchange accounts, or only in dev/debug environments
            var allAccounts = await service.GetAllAsync(userId: null!, ct); // pass null or special value to indicate all
            return Results.Ok(allAccounts);
        });
        
        endpoints.MapGet(Routes.ExchangeAccountsPaginated,
            async ([FromServices] IExchangeAccountService service,
                [AsParameters] ExchangeAccountQueryParams query,
                CancellationToken ct) =>
            {
                var result = await service.GetPaginatedAsync(query, ct);
                return Results.Ok(result);
            });
        
        endpoints.MapGet(Routes.ExchangeAccountsByUserId, 
            async ([FromServices] IExchangeAccountService service, [AsParameters] ExchangeAccountQueryParams query, CancellationToken ct) =>
        {
            var accounts = await service.GetAllAsync(query.UserId.ToString(), ct);
            return Results.Ok(accounts);
        });

        endpoints.MapGet(Routes.ExchangeAccountsById, async (Guid id, [FromServices] IExchangeAccountService service, CancellationToken ct) =>
        {
            var account = await service.GetByIdAsync(id, ct);
            return account is null ? Results.NotFound() : Results.Ok(account);
        });
        
        endpoints.MapPost(Routes.ExchangeAccounts, async (
            ExchangeAccountCreateModel model,
            [FromServices] IExchangeAccountService service,
            // [FromServices] IUnitOfWork unitOfWork,
            // [FromServices] IExchangeSyncService syncService,
            CancellationToken ct) =>
        {
            var result = await service.CreateExchangeAccountAsync(model, ct);

            // var createdEntity = await unitOfWork.ExchangeAccounts.GetByIdAsync(result.Id, ct);
            // if (createdEntity is not null)
            //     await syncService.SyncAccountAsync(createdEntity, ct);

            return Results.Created($"{Routes.ExchangeAccounts}/{result.Id}", result);
        });
        
        endpoints.MapPost(Routes.ExchangeAccountsSync, async (
            [FromQuery] Guid id,
            [FromServices] IExchangeSyncService syncService,
            CancellationToken ct) =>
        {
            var result = await syncService.SyncAccountByIdAsync(id, ct);
            return result ? Results.Ok() : Results.NotFound();
        });
        
        endpoints.MapPut(Routes.ExchangeAccountsById, async (Guid id, ExchangeAccountDto dto, [FromServices] IExchangeAccountService service, CancellationToken ct) =>
        {
            var result = await service.UpdateAsync(id, dto, ct);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });

        endpoints.MapDelete(Routes.ExchangeAccountsById, async (Guid id, [FromServices] IExchangeAccountService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });
    }
}