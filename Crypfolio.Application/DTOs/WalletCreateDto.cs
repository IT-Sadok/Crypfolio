namespace Crypfolio.Application.DTOs;

public class WalletCreateDto
{
    public string Name { get; set; }
    public string Blockchain { get; set; }
    public string? Address { get; set; }
}