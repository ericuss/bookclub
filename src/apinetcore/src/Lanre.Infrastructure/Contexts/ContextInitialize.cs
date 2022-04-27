using Microsoft.EntityFrameworkCore;

namespace Lanre.Infrastructure.Contexts
{
    public static class ContextInitialize
    {
        public static async Task InitializeDb<T>(SqlOptions options, T context)
            where T : DbContext
        {
            if (options.Migrate)
            {
                if (options.EnsureDeleted)
                {
                    await context.Database.EnsureDeletedAsync();
                }
                context.Database.Migrate();
            }
        }
    }
}
