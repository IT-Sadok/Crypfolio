namespace Crypfolio.Domain.Entities;

public class RefreshToken {
    public Guid Id { get; set; }
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedAt { get; set; }
    
    public string DeviceId { get; set; } = default!; 
    public string DeviceName { get; set; } = default!; // optionally, for UI or debugging
    public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;
}
