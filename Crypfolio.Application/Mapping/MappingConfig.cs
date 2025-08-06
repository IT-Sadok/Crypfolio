using Mapster;
using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Mapping;
public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<AssetCreateModel, Asset>.NewConfig()
            .Map(asset => asset.Ticker, dto => LowerCaseValue(dto.Ticker))
            .IgnoreNullValues(true);
        TypeAdapterConfig<Asset, AssetDto>.NewConfig()
            .Map(dest => dest.Ticker, src => LowerCaseValue(src.Ticker))
            .IgnoreNullValues(true);
        TypeAdapterConfig<AssetDto, Asset>.NewConfig()
            .Map(dest => dest.Ticker, src => LowerCaseValue(src.Ticker))            
            .IgnoreNullValues(true);

    }
    
    private static string? LowerCaseValue(string value)
    {
        return value.ToLowerInvariant();
    }
}