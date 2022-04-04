using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Mc2.CrudTest.Persistence.Context
{
    public class Mc2DbContextFactory : IDesignTimeDbContextFactory<Mc2Context>
    {
        public Mc2Context CreateDbContext(string[] args)
        {
            string basePath = Directory.GetCurrentDirectory();

            IConfigurationRoot? configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                //.AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            DbContextOptionsBuilder<Mc2Context> builder = new DbContextOptionsBuilder<Mc2Context>();
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new Mc2Context(builder.Options);
        }
    }
}
