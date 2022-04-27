using Lanre.Context.Poll.Domain;
using Lanre.Infrastructure.Contexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Lanre.Context.Poll.Infrastructure.Database
{
    public class PollContext : ContextCore<PollContext>
    {
        public PollContext(DbContextOptions<PollContext> options, IOptions<SqlOptions> sqlOptions) 
            : base(options, sqlOptions)
        {
        }

        //public DbSet<Book> Books { get; set; }

        public DbSet<VoteList> VoteLists { get; set; }
    }
}
