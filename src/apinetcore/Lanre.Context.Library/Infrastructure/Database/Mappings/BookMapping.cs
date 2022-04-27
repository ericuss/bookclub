namespace Lanre.Context.Library.Infrastructure.Database.Mappings;

using Lanre.Context.Library.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookMapping : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books", "library");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(500).IsRequired();

        builder.HasIndex(x => x.Name).IsUnique();
        Data(builder);
    }

    public void Data(EntityTypeBuilder<Book> builder)
    {
        builder.HasData(
            Book.Create(Guid.Parse("8bddba00-f200-402d-b45b-6f1634a5f622"), "El imperio final"),
            Book.Create(Guid.Parse("332fb5ab-2eab-4393-a920-9b46faed3cb5"), "El juego de Ender")
            );
    }
}

