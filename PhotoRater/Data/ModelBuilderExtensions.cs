using Microsoft.EntityFrameworkCore;

namespace Task11.Data;

public static class ModelBuilderExtensions
{
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<OperationType>().HasData(DefaultOperationTypes);
    }
}