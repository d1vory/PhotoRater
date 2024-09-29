using Microsoft.EntityFrameworkCore;
using PhotoRater.Models.Directory;

namespace Task11.Data;

public static class ModelBuilderExtensions
{
    public static Status[] DefaultStatuses = new[]
    {
        new Status() { Id = 1, Name = "On review" },
        new Status() { Id = 2, Name = "Inactive" },
        new Status() { Id = 3, Name = "Reviewed" },
        new Status() { Id = 4, Name = "Deleted" },
    };
    
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Status>().HasData(DefaultStatuses);
    }
}