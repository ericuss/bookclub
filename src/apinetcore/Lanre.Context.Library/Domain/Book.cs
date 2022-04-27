namespace Lanre.Context.Library.Domain;

public class Book
{
    private Book()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; internal set; }

    public string Name { get; private set; }

    public Book SetName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }
        Name = name;
        return this;
    }

    internal static Book Create(Guid id, string? name)
    {
        var entity = Create(name);
        entity.Id = id;
        return entity;
    }
    public static Book Create(string? name)
    {
        var entity = new Book();

        return entity.SetName(name);
    }
}