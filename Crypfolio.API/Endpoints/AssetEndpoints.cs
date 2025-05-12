using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;

namespace Crypfolio.Api.Endpoints;

public static class AssetEndpoints
{
    public static void MapAssetEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/assets/", async (IAssetService service, CancellationToken cancellationToken) =>
        {
            var assets = await service.GetAllAsync(cancellationToken);
            return !assets.Any() ? Results.NotFound() : Results.Ok(assets);
        });
        
        app.MapGet("/api/assets/by-id/{id}", async (Guid id, IAssetService service, CancellationToken cancellationToken) =>
        {
            var asset = await service.GetByIdAsync(id, cancellationToken);
            return asset is null ? Results.NotFound() : Results.Ok(asset);
        });
        
        app.MapGet("/api/assets/by-symbol/{symbol}", async (string symbol, IAssetService service, CancellationToken cancellationToken) =>
        {
            var asset = await service.GetBySymbolAsync(symbol, cancellationToken);
            return asset is null ? Results.NotFound() : Results.Ok(asset);
        });

        app.MapPost("/api/assets", async (CreateAssetDto dto, IAssetService service, CancellationToken cancellationToken) =>
        {
            await service.AddAsync(dto, cancellationToken);
            return Results.Ok();
        });

        app.MapPut("/api/assets/{symbol}", async (string symbol, AssetDto dto, IAssetService service, CancellationToken cancellationToken) =>
        {
            var asset = await service.GetBySymbolAsync(symbol, cancellationToken);
            if (asset is null)
                return Results.NotFound();

            asset.Name = dto.Name;
            asset.Balance = dto.Balance;
            asset.AverageBuyPrice = dto.AverageBuyPrice;

            await service.UpdateAsync(asset, cancellationToken);
            return Results.NoContent();
        });
        
        app.MapDelete("/api/assets/{symbol}", async (string symbol, IAssetService service, CancellationToken cancellationToken) =>
        {
            await service.DeleteAsync(symbol, cancellationToken);
            return Results.Ok();
        });
    }
}