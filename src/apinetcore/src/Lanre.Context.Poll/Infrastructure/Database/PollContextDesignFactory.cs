using Lanre.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Lanre.Context.Poll.Infrastructure.Database
{
    public class PollContextDesignFactory : IDesignTimeDbContextFactory<PollContext>
    {
        public PollContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.Development.json", optional: true)
                            .AddEnvironmentVariables()
                            .Build();

            var settings = config.GetSection("Sql").Get<SqlOptions>();
            var optionsBuilder = new DbContextOptionsBuilder<PollContext>();

            optionsBuilder.UseSqlServer(settings.ConnectionString);

            return new PollContext(optionsBuilder.Options, Options.Create(settings));
        }
    }
}
