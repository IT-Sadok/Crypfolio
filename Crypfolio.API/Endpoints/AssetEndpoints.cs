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
        
        app.MapGet(Routes.AssetsByTicker, async (string ticker, [FromServices] IAssetService service, CancellationToken ct) =>
        {
            var asset = await service.GetByTickerAsync(ticker, ct);
            return asset is null ? null : Results.Ok(asset);
        });

        app.MapPost(Routes.Assets, async (AssetCreateDto dto, [FromServices] IAssetService service, CancellationToken ct) =>
        {
            await service.AddAsync(dto, ct);
            return Results.Ok();
        });

        app.MapPut(Routes.AssetsByTicker, async (
            string ticker, AssetDto dto, [FromServices] IAssetService service, CancellationToken ct) =>
        {
            var result = await service.UpdateByTickerAsync(ticker, dto, ct);
            return result.IsSuccess
                ? Results.Ok()
                : Results.NotFound(new { error = result.Error });
        });
        
        app.MapDelete(Routes.AssetsByTicker, async (string ticker, [FromServices] IAssetService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(ticker, ct);
            return Results.Ok();
        });
    }
}