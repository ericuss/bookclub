using Lanre.Context.Users.Domain;
using Lanre.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Lanre.Context.Users.Infrastructure.Database
{
    public class UsersContext : ContextCore<UsersContext>
    {
        public UsersContext(DbContextOptions<UsersContext> options, IOptions<SqlOptions> sqlOptions) 
            : base(options, sqlOptions)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
