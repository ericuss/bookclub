using Lanre.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Lanre.Context.Users.Infrastructure.Database
{
    public class UsersContextDesignFactory : IDesignTimeDbContextFactory<UsersContext>
    {
        public UsersContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.Development.json", optional: true)
                            .AddEnvironmentVariables()
                            .Build();

            var settings = config.GetSection("Sql").Get<SqlOptions>();
            var optionsBuilder = new DbContextOptionsBuilder<UsersContext>();

            optionsBuilder.UseSqlServer(settings.ConnectionString);

            return new UsersContext(optionsBuilder.Options, Options.Create(settings));
        }
    }
}
