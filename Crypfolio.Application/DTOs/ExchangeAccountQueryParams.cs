namespace Crypfolio.Application.DTOs;

public class ExchangeAccountQueryParams
{
    public Guid? UserId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}