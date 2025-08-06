using System.Net;
using System.Net.Http.Json;
using Crypfolio.Application.Interfaces;
using Crypfolio.Infrastructure.Persistence;
using Crypfolio.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Testcontainers.MsSql;

namespace Crypfolio.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer;
   
    public CustomWebApplicationFactory()
    {
        _dbContainer = new MsSqlBuilder()
            //.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithImage("mcr.microsoft.com/azure-sql-edge")
            //.WithName("crypfolio-test-db")
            .WithPassword("yourStrong(!)Password")
            .WithCleanUp(true)
            .Build();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove existing DbContext
            var descriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_dbContainer.GetConnectionString()));
            
            // Register HttpClient with mocked handler for BinanceApiService
            services.AddHttpClient<BinanceApiService>()
                .ConfigurePrimaryHttpMessageHandler(() => CreateMockBinanceHttpHandler());

            // Register IExchangeApiService for DI resolution
            services.AddSingleton<IExchangeApiService>(provider =>
                provider.GetRequiredService<BinanceApiService>());

            services.AddLogging();
        });
    }
    
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
    }
    
    // Mock HTTP response that BinanceApiService would receive
    private static HttpMessageHandler CreateMockBinanceHttpHandler()
    {
        var fakeResponse = new BinanceAccountInfoDto
        {
            Balances = new List<BinanceBalanceDto>
            {
                new() { Asset = "BTC", Free = "0.10", Locked = "0.20" },
                new() { Asset = "ETH", Free = "1.00", Locked = "1.00" }
            }
        };

        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(fakeResponse)
        };

        return new MockHttpMessageHandler(mockResponse);
    }

    public async Task DisposeAsync() => await _dbContainer.StopAsync();
}