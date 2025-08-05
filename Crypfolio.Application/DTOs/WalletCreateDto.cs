using Crypfolio.Domain.Enums;

namespace Crypfolio.Application.DTOs;

public class WalletCreateDto
{
    public string Name { get; set; }
    public string? Address { get; set; }
    public WalletType WalletType { get; set; } = WalletType.Unknown;
}