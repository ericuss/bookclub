using Lanre.Context.Library.Infrastructure.Database;
using Lanre.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lanre.Context.Library;
public static class Configure
{
    private const string SqlSectionKey = "Sql";
    public static IServiceCollection ConfigureLibrary(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlOptions = configuration.GetSection(SqlSectionKey).Get<SqlOptions>();
        return services
                .Configure<SqlOptions>(configuration.GetSection(SqlSectionKey))
                .RegisterDB(sqlOptions)
                ;
    }

    private static IServiceCollection RegisterDB(this IServiceCollection services, SqlOptions sqlOptions)
    {
        services.AddDbContext<LibraryContext>(o => o.UseSqlServer(sqlOptions.ConnectionString, x =>
            x.EnableRetryOnFailure(5, TimeSpan.FromSeconds(2000), null)
        ), ServiceLifetime.Transient);

        return services;
    }
}
