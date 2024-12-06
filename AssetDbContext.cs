using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;  // Add this line
using System.Collections.Generic;

public class AssetDbContext : DbContext
{
    // Define the table in our database
    public DbSet<Asset> Assets { get; set; }

    // Configure the database connection
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // This connection string uses LocalDB, which comes with Visual Studio
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=AssetManagerDB;Trusted_Connection=True;");
    }
}