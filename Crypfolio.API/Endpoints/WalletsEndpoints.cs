using Crypfolio.API.Constants;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;

namespace Crypfolio.API.Endpoints;

public static class WalletEndpoints
{
    public static void MapWalletEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(Routes.WalletsByUserId, async (string userId, IWalletService service, CancellationToken ct) =>
        {
            var wallets = await service.GetAllAsync(userId, ct);
            return Results.Ok(wallets);
        });

        endpoints.MapGet(Routes.WalletsById, async (Guid id, IWalletService service, CancellationToken ct) =>
        {
            var wallet = await service.GetByIdAsync(id, ct);
            return wallet is null ? null : Results.Ok(wallet);
        });

        endpoints.MapPost(Routes.Wallets, async (WalletDto dto, IWalletService service, CancellationToken ct) =>
        {
            await service.AddAsync(dto, ct);
            return Results.Created($"{Routes.Wallets}/{dto.Id}", dto);
        });

        endpoints.MapPut(Routes.WalletsById, async (Guid id, WalletDto dto, IWalletService service, CancellationToken ct) =>
        {
            var result = await service.UpdateAsync(id, dto, ct);
            return result.IsSuccess ? Results.Ok() : Results.BadRequest(result.Error);
        });

        endpoints.MapDelete(Routes.WalletsById, async (Guid id, IWalletService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });
    }
}