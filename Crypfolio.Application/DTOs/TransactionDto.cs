using Crypfolio.Domain.Enums;

namespace Crypfolio.Application.DTOs;

public class TransactionDto
{
    public Guid Id { get; set; }
    public Guid AssetId { get; set; } 

    public Guid? WalletId { get; set; }
    public Guid? AccountId { get; set; }
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}