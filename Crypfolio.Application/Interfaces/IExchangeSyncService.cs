using Crypfolio.Application.DTOs;
using Crypfolio.Domain.Entities;

namespace Crypfolio.Application.Interfaces;

public interface IExchangeSyncService
{
    Task SyncAccountAsync(ExchangeAccount account, CancellationToken cancellationToken);
}