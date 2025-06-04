namespace Crypfolio.Application.DTOs;

public class ExchangeAccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ApiKeyEncrypted { get; set; } = string.Empty;
    public string? ApiSecretEncrypted { get; set; } = string.Empty;
    public string? ApiPassphrase { get; set; } = string.Empty;
}