using Crypfolio.Domain.Abstract;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Domain.Entities;

public class ExchangeAccount : AccountSource
{
    public ExchangeName ExchangeName { get; set; }
    public string? ApiKeyEncrypted { get; set; } = string.Empty;
    public string? ApiSecretEncrypted { get; set; } = string.Empty;
    public string? ApiPassphrase { get; set; } = string.Empty;
}