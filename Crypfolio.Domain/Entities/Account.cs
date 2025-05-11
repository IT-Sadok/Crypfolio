namespace Crypfolio.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Exchange { get; set; } = string.Empty;
    public string ApiKeyEncrypted { get; set; } = string.Empty;
    public string ApiSecretEncrypted { get; set; } = string.Empty;
    
    public List<Asset>? Assets { get; set; }
    public List<Transaction>? Transactions { get; set; }
    
    public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
}