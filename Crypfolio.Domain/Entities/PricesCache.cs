namespace Crypfolio.Domain.Entities;

public class PricesCache
{
    public Guid Id { get; set; }
    public string Symbol { get; set; } = string.Empty; 
    public decimal PriceUsd { get; set; }
    
    public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
}