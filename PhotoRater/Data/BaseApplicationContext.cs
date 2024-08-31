using Microsoft.EntityFrameworkCore;
using Task11.Data;

public class BaseApplicationContext: DbContext
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}