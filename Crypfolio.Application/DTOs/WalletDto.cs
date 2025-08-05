using Crypfolio.Domain.Enums;

namespace Crypfolio.Application.DTOs;

public class WalletDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public WalletType WalletType { get; set; } = WalletType.Unknown;
}