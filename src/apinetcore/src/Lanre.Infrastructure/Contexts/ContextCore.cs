using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Lanre.Infrastructure.Contexts;

public abstract class ContextCore<T> : DbContext
    where T : DbContext
{
    private readonly SqlOptions _sqlOptions;

    public ContextCore(DbContextOptions<T> options, IOptions<SqlOptions> sqlOptions)
        : base(options)
    {
        _sqlOptions = sqlOptions.Value;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_sqlOptions.DetailedErrors)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        base.OnConfiguring(optionsBuilder);
    }
}
