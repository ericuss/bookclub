using Lanre.Context.Library.Domain;
using Lanre.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Lanre.Context.Library.Infrastructure.Database
{
    public class LibraryContext : ContextCore<LibraryContext>
    {
        public LibraryContext(DbContextOptions<LibraryContext> options, IOptions<SqlOptions> sqlOptions) 
            : base(options, sqlOptions)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
