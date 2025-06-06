using Crypfolio.API.Constants;
using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crypfolio.Api.Endpoints;

public static class AssetEndpoints
{   
    public static void MapAssetEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(Routes.Assets, async ([FromServices] IAssetService service, CancellationToken ct) =>
        {
            var assets = await service.GetAllAsync(ct);
            return !assets.Any() ? null : Results.Ok(assets);
        });
        
        app.MapGet(Routes.AssetsById, async (Guid id, [FromServices] IAssetService service, CancellationToken ct) =>
        {
            var asset = await service.GetByIdAsync(id, ct);
            return asset is null ? null : Results.Ok(asset);
        });
        
        app.MapGet(Routes.AssetsBySymbol, async (string symbol, [FromServices] IAssetService service, CancellationToken ct) =>
        {
            var asset = await service.GetBySymbolAsync(symbol, ct);
            return asset is null ? null : Results.Ok(asset);
        });

        app.MapPost(Routes.Assets, async (CreateAssetDto dto, [FromServices] IAssetService service, CancellationToken ct) =>
        {
            await service.AddAsync(dto, ct);
            return Results.Ok();
        });

        app.MapPut(Routes.AssetsBySymbol, async (
            string symbol, AssetDto dto, [FromServices] IAssetService service, CancellationToken ct) =>
        {
            var result = await service.UpdateBySymbolAsync(symbol, dto, ct);
            return result.IsSuccess
                ? Results.Ok()
                : Results.NotFound(new { error = result.Error });
        });
        
        app.MapDelete(Routes.AssetsBySymbol, async (string symbol, [FromServices] IAssetService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(symbol, ct);
            return Results.Ok();
        });
    }
}