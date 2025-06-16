using Crypfolio.Domain.Entities;

namespace Crypfolio.Domain.Abstract;

public abstract class AccountSource
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = new();
    public string AccountName { get; set; } = string.Empty;

    public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}