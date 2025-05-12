using Mapster;
using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Mapping;
public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<CreateAssetDto, Asset>.NewConfig();
        TypeAdapterConfig<Asset, AssetDto>.NewConfig();
        TypeAdapterConfig<AssetDto, Asset>.NewConfig();
    }
}