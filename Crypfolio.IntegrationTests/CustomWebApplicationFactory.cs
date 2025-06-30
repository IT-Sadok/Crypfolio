using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Enums;
using Crypfolio.Infrastructure.Persistence;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NSubstitute;
using Microsoft.AspNetCore.Hosting;
using Testcontainers.MsSql;

namespace Crypfolio.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _dbContainer;
    public IExchangeApiService BinanceApiMock { get; private set; } = default!;
   
    public CustomWebApplicationFactory()
    {
        _dbContainer = new MsSqlBuilder()
            //.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithImage("mcr.microsoft.com/azure-sql-edge")
            //.WithName("crypfolio-test-db")
            .WithPassword("yourStrong(!)Password")
            // .WithWaitStrategy(Wait.ForUnixContainer()
            //     .UntilPortIsAvailable(1433)
            //     .UntilMessageIsLogged("Recovery is complete."))
            //.WithCreateParameterModifier(cmd => cmd.Cmd("linux/amd64"))
            //.WithPlatform("linux/amd64") // - to fix the issue with Mac chip
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

            // Mocks external services
            BinanceApiMock = Substitute.For<IExchangeApiService>();
            BinanceApiMock.ExchangeName.Returns(ExchangeName.Binance);
            services.AddSingleton<IExchangeApiService>(BinanceApiMock);
        });
    }
    
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
    }

    public async Task DisposeAsync() => await _dbContainer.StopAsync();
}