using Microsoft.AspNetCore.Identity;

namespace Crypfolio.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}