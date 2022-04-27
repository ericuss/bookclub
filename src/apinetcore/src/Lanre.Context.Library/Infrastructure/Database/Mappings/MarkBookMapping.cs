namespace Lanre.Context.Library.Infrastructure.Database.Mappings;

using Lanre.Context.Library.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MarkBookMapping : IEntityTypeConfiguration<MarkBook>
{
    public void Configure(EntityTypeBuilder<MarkBook> builder)
    {
        builder.ToTable("MarkBooks", "library");
        builder.HasKey(x => new { x.BookId, x.UserId, x.Marked });
        Data(builder);
    }

    public static void Data(EntityTypeBuilder<MarkBook> builder)
    {
        var markBook1 = new MarkBook.Builder()
                .SetBookId(Guid.Parse("8bddba00-f200-402d-b45b-6f1634a5f622"))
                .SetUserId("251963be-2c3e-435f-9da7-a62bec3d508a")
                .SetMarked(MarkBookTypes.Readed)
                .Build();

        builder.HasOne(x => x.Book)
                .WithMany()
                .HasForeignKey(x => x.BookId)
                .IsRequired();

        builder.HasData(markBook1);
    }
}

