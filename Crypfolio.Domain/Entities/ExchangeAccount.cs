using Crypfolio.Domain.Entities;
using Crypfolio.Domain.Enums;

namespace Crypfolio.Domain.Entities;

public class ExchangeAccount
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } 
    public string AccountName { get; set; } = string.Empty;

    public ExchangeName ExchangeName { get; set; }
    public string? ApiKeyEncrypted { get; set; } = string.Empty;
    public string? ApiSecretEncrypted { get; set; } = string.Empty;
    public string? ApiPassphrase { get; set; } = string.Empty;
    
    public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
}