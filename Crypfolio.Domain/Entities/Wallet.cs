using Crypfolio.Domain.Abstract;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Domain.Entities;

public class Wallet : AccountSource
{
    public string Address { get; set; } = string.Empty;
    public string Blockchain { get; set; } = string.Empty;
    public WalletType WalletType { get; set; } = WalletType.Unknown;
}