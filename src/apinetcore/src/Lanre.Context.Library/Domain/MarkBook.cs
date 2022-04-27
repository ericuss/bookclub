namespace Lanre.Context.Library.Domain;

public enum MarkBookTypes
{
    None = 0,
    Readed = 10,
    Ignore = 20,
    Like = 30,
    Unlike = 40
}

public class MarkBook
{
    private MarkBook()
    {
    }

    public Guid BookId { get; internal set; }

    public string UserId { get; internal set; }

    public MarkBookTypes Marked { get; internal set; }

    public virtual Book Book { get; internal set; }

    public MarkBook SetBookId(Guid? bookId)
    {
        if (!bookId.HasValue)
        {
            throw new ArgumentException($"'{nameof(bookId)}' cannot be null or whitespace.", nameof(bookId));
        }
        BookId = bookId.Value;
        return this;
    }

    public MarkBook SetUserId(string? userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException($"'{nameof(userId)}' cannot be null or whitespace.", nameof(userId));
        }
        UserId = userId;
        return this;
    }

    public MarkBook SetMarked(MarkBookTypes marked)
    {
        if (marked == MarkBookTypes.None)
        {
            throw new ArgumentException($"'{nameof(marked)}' cannot be null or whitespace.", nameof(marked));
        }

        Marked = marked;
        return this;
    }

    public class  Builder
    {
        private Guid? bookId;
        private string? userId;
        private MarkBookTypes marked; 

        public Builder SetBookId(Guid? bookId)
        {
            this.bookId = bookId;
            return this;
        }

        public Builder SetUserId(string? userId)
        {
            this.userId = userId;
            return this;
        }

        public Builder SetMarked(MarkBookTypes marked)
        {
            this.marked = marked;
            return this;
        }

        public MarkBook Build()
        {
            var entity = new MarkBook()
                            .SetBookId(this.bookId)
                            .SetUserId(this.userId)
                            .SetMarked(this.marked);
            return entity;
        }
    }

}