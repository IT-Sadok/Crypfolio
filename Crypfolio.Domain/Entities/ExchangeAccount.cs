using Crypfolio.Domain.Abstract;

namespace Crypfolio.Domain.Entities;

public class ExchangeAccount : AccountSource
{
    public string? ApiKeyEncrypted { get; set; } = string.Empty;
    public string? ApiSecretEncrypted { get; set; } = string.Empty;
    public string? ApiPassphrase { get; set; } = string.Empty;
}