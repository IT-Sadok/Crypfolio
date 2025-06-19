using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Crypfolio.Infrastructure.Persistence;
using Crypfolio.Infrastructure.Extensions;
using Crypfolio.Application.Extensions;
using Xunit;
using Microsoft.Extensions.Logging;

namespace Crypfolio.IntegrationTests;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    protected IServiceScopeFactory ScopeFactory = default!;
    protected IServiceProvider ServiceProvider = default!;
    private IServiceScope _scope = default!;
    private readonly ILogger<IntegrationTestBase> _logger;

    protected IntegrationTestBase(ILogger<IntegrationTestBase> logger)
    {
        _logger = logger;
    }

    protected ApplicationDbContext DbContext => GetService<ApplicationDbContext>();
    protected T GetService<T>() => _scope.ServiceProvider.GetRequiredService<T>();

    public async Task InitializeAsync()
    {
        var services = new ServiceCollection();

        var testConfig = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Test.json", optional: false)
            .Build();
        
        var connStr = testConfig.GetConnectionString("DefaultConnection");
        _logger.LogInformation("🔌 Using test DB connection string: " + connStr);
        
        services.AddInfrastructure(testConfig);
        services.AddApplication();

        ServiceProvider = services.BuildServiceProvider();
        ScopeFactory = ServiceProvider.GetRequiredService<IServiceScopeFactory>();
        _scope = ScopeFactory.CreateScope();

        var context = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        await SeedDataAsync(context);
    }

    public Task DisposeAsync()
    {
        _scope.Dispose();
        if (ServiceProvider is IDisposable d)
            d.Dispose();
        return Task.CompletedTask;
    }

    protected virtual Task SeedDataAsync(ApplicationDbContext dbContext) => Task.CompletedTask;
}