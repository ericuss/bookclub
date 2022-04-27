namespace Lanre.Infrastructure.Contexts;

public class SqlOptions
{
    public string ConnectionString { get; set; }

    public bool DetailedErrors { get; set; }

    public bool Migrate { get; set; }

    public bool EnsureDeleted { get; set; }

    public bool ApplySeed { get; set; }

}
