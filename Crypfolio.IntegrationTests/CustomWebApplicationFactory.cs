using Crypfolio.Application.Interfaces;
using Crypfolio.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NSubstitute;

namespace Crypfolio.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public IUnitOfWork UnitOfWorkMock { get; private set; } = default!;
    public IBinanceApiService BinanceApiMock { get; private set; } = default!;
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove real DB context registration
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (dbContextDescriptor != null)
                services.Remove(dbContextDescriptor);

            // Use test DB
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=BEAMPC\\SQLEXPRESS;Database=CrypfolioTestDb;Trusted_Connection=True;TrustServerCertificate=True"));

            // Create NSubstitute mocks
            BinanceApiMock = Substitute.For<IBinanceApiService>();
            //UnitOfWorkMock = Substitute.For<IUnitOfWork>();

            // Replace service with mock
            services.RemoveAll<IBinanceApiService>();
            services.AddSingleton(BinanceApiMock);
            
            // services.RemoveAll<IUnitOfWork>();
            // services.AddSingleton(UnitOfWorkMock);
        });

        return base.CreateHost(builder);
    }
}