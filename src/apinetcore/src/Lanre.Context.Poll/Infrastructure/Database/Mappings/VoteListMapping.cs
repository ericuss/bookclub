namespace Lanre.Context.Poll.Infrastructure.Database.Mappings;

using Lanre.Context.Poll.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VoteListMapping : IEntityTypeConfiguration<VoteList>
{
    public void Configure(EntityTypeBuilder<VoteList> builder)
    {
        builder.ToTable("VoteList", "Poll");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500).IsRequired();

        builder.HasIndex(x => x.Name).IsUnique();
        Data(builder);
    }

    public static void Data(EntityTypeBuilder<VoteList> builder)
    {
        var poll1 = new VoteList.Builder()
                .SetName("May 2022 Poll")
                .SetUserId("0000000000")
                .Build();
        poll1.Id = Guid.Parse("8bddba00-f200-402d-b45b-6f1634a5f622");

        var poll2 = new VoteList.Builder()
                .SetName("June 2022 Poll")
                .SetUserId("0000000000")
                .Build();
        poll2.Id = Guid.Parse("332fb5ab-2eab-4393-a920-9b46faed3cb5");

        builder.HasData(
            poll1,
            poll2
            );
    }
}

