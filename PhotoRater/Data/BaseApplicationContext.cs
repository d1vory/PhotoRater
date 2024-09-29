using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoRater.Models;
using PhotoRater.Models.Directory;
using Task11.Data;

public class BaseApplicationContext: IdentityDbContext<User>
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PhotoOnRate> PhotosOnRate { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    
    
    public DbSet<Status> Statuses { get; set; }
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Seed();
    }
    
    public override int SaveChanges()
    {
        UpdateCreateAt();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateCreateAt();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateCreateAt()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is PhotoOnRate && (
                e.State == EntityState.Added
            ));

        foreach (var entry in entries)
        {
            var entity = (PhotoOnRate)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.UtcNow;
            }

            entity.CreatedAt = DateTime.UtcNow;
        }
    }

}
