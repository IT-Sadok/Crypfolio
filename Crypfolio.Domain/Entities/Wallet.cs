using Crypfolio.Domain.Abstract;

namespace Crypfolio.Domain.Entities;

public class Wallet : AccountSource
{
    public string Address { get; set; } = string.Empty;
    public string Blockchain { get; set; } = string.Empty;
    public string WalletType { get; set; } = string.Empty; // e.g., "MetaMask", "Ledger"
}