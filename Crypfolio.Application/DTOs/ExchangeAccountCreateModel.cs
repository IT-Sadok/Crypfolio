using Crypfolio.Domain.Enums;

namespace Crypfolio.Application.DTOs;

public class ExchangeAccountCreateModel
{
    public string UserId { get; set; } = default!;
    public string AccountName { get; set; } = default!;
    public ExchangeName ExchangeName{ get; set; }
    public string? ApiKey { get; set; }
    public string? ApiSecret { get; set; }
    public string? ApiPassphrase { get; set; }
}