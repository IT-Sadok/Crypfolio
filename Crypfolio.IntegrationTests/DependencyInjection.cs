using Crypfolio.Application.Interfaces;
using Crypfolio.Application.Services;
using Crypfolio.Infrastructure.Persistence;
using Crypfolio.IntegrationTests.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Crypfolio.IntegrationTests;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IBinanceApiService, MockBinanceApiService>(); // for mocking

        return services;
    }
}
