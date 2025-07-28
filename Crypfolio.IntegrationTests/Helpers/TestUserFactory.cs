using Crypfolio.Domain.Entities;
using Crypfolio.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Crypfolio.IntegrationTests.Helpers;

public static class TestUserFactory
{
    public static async Task<ApplicationUser> GetOrCreateTestUserAsync(ApplicationDbContext db)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.UserName.Contains("testuser"));
        if (user != null) return user;
        
        user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "testuser_" + Guid.NewGuid(),
            Email = "testuser@example.com"
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();

        return user;
    }
}