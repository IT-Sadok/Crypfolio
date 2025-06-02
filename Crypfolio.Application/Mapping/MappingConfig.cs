using Mapster;
using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Mapping;
public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<CreateAssetDto, Asset>.NewConfig()
            .Map(asset => asset.Symbol, dto => LowerCaseValue(dto.Symbol))
            .IgnoreNullValues(true);
        TypeAdapterConfig<Asset, AssetDto>.NewConfig()
            .Map(dest => dest.Symbol, src => LowerCaseValue(src.Symbol))
            .IgnoreNullValues(true);
        TypeAdapterConfig<AssetDto, Asset>.NewConfig()
            .Map(dest => dest.Symbol, src => LowerCaseValue(src.Symbol))            
            .IgnoreNullValues(true);

    }
    
    private static string? LowerCaseValue(string value)
    {
        return value.ToLowerInvariant();
    }
}