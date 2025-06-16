using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Crypfolio.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Crypfolio.IntegrationTests.Tests;

public class ExchangeAccountServiceTests : IntegrationTestBase
{
    [Fact]
    public async Task CreateExchangeAccount_CreatesAccountSuccessfully()
    {
        // Arrange
        var service = GetService<IExchangeAccountService>();
        var userId = Guid.NewGuid().ToString();

        var dto = new ExchangeAccountCreateDto()
        {
            UserId = userId,
            AccountName = "Binance Test Account",
            ExchangeName = ExchangeName.Binance,
            ApiKey = "test-api-key",
            ApiSecret = "test-api-secret"
        };

        // Act
        var result = await service.CreateExchangeAccountAsync(dto, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.AccountName.Should().Be(dto.AccountName);
        result.ExchangeName.Should().Be(ExchangeName.Binance);
    }
}