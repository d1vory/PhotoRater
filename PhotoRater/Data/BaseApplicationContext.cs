using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoRater.Areas.Identity.Data;
using Task11.Data;

public class BaseApplicationContext: IdentityDbContext<User>
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Seed();
    }
}