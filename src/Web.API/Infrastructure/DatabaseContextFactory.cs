using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Web.API.Infrastructure
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseNpgsql("User ID=postgres;Password=password;Host=localhost;Port=5432;Database=recordDb;");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
