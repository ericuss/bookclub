namespace Lanre.Context.Poll;
using Lanre.Context.Poll.Infrastructure.Database;
using Lanre.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Configure
{
    private const string SqlSectionKey = "Sql";
    public static IServiceCollection ConfigurePoll(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlOptions = configuration.GetSection(SqlSectionKey).Get<SqlOptions>();
        return services
                .Configure<SqlOptions>(configuration.GetSection(SqlSectionKey))
                .RegisterDB(sqlOptions)
                ;
    }

    private static IServiceCollection RegisterDB(this IServiceCollection services, SqlOptions sqlOptions)
    {
        services.AddDbContext<PollContext>(o => o.UseSqlServer(sqlOptions.ConnectionString, x =>
            x.EnableRetryOnFailure(5, TimeSpan.FromSeconds(2000), null)
        ), ServiceLifetime.Transient);

        return services;
    }
}
