namespace Crypfolio.Application.DTOs;

public class CreateExchangeAccountRequestDto
{
    public string Name { get; set; }
    public string ExchangeName { get; set; }
    public string? ApiKey { get; set; }
    public string? ApiSecret { get; set; }
    public string? ApiPassphrase { get; set; }
}