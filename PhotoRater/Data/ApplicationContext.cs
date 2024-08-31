using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Task11.Data;

public class ApplicationContext: BaseApplicationContext
{
    protected readonly IConfiguration _configuration;

    public ApplicationContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
}