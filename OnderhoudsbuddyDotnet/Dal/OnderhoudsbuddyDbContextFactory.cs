using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dal
{
    public class OnderhoudsbuddyDbContextFactory : IDesignTimeDbContextFactory<OnderhoudsbuddyDbContext>
    {
        public OnderhoudsbuddyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OnderhoudsbuddyDbContext>();

            // Stel hier de juiste connection string in
            optionsBuilder.UseMySql(
                "Server=localhost;Database=onderhoudsbuddydotnet;User=root;Password=yuc2426Horp35@;",
                new MySqlServerVersion(new Version(8, 0, 23))
            );

            return new OnderhoudsbuddyDbContext(optionsBuilder.Options);
        }
    }
}
