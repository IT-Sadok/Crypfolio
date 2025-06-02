namespace Crypfolio.Domain.Entities;

public class Wallet
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = new();
    
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Blockchain { get; set; } = string.Empty;
    
    public List<Asset> Assets { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
}