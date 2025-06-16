using Crypfolio.Domain.Enums;

namespace Crypfolio.Application.DTOs;

public class ExchangeAccountDto
{
    public string UserId { get; set; } = default!;
    public Guid Id { get; set; }
    public string AccountName { get; set; } = string.Empty;
    public ExchangeName ExchangeName { get; set; }
    public string? ApiKeyEncrypted { get; set; } = string.Empty;
    public string? ApiSecretEncrypted { get; set; } = string.Empty;
    public string? ApiPassphrase { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}