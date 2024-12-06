using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;  

public class AssetDbContext : DbContext
{
    // Define the table in our database
    public DbSet<Asset> Assets { get; set; }

    // Configure the database connection
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=AssetManagerDB;Trusted_Connection=True;");
    }
}