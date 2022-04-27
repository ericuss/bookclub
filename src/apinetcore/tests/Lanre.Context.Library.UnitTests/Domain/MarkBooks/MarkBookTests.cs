using FluentAssertions;

using Lanre.Context.Library.Domain;

using System;

using Xunit;

namespace Lanre.Context.Library.UnitTests.Domain.Books;

public class MarkBookTests
{
    [Fact]
    public void Entity_Is_Created_When_All_Fields_Was_Filled()
    {
        var bookId = Guid.NewGuid();
        var userId = Guid.NewGuid().ToString();
        var marked = MarkBookTypes.Readed;

        var entity = new MarkBook.Builder()
                     .SetBookId(bookId)
                     .SetUserId(userId)
                     .SetMarked(marked)
                     .Build();

        entity.Should().NotBeNull();
        entity.BookId.Should().Be(bookId);
        entity.UserId.Should().Be(userId);
        entity.Marked.Should().Be(marked);
    }

    [Fact]
    public void Entity_Is_Not_Created_When_BookId_Is_Empy()
    {
        Guid? bookId = null;
        var userId = Guid.NewGuid().ToString();
        var marked = MarkBookTypes.Readed;


        Assert.Throws<ArgumentException>(() => new MarkBook.Builder()
                     .SetBookId(bookId)
                     .SetUserId(userId)
                     .SetMarked(marked)
                     .Build());
    }

    [Fact]
    public void Entity_Is_Not_Created_When_UserId_Is_Empy()
    {
        var bookId = Guid.NewGuid();
        string? userId = string.Empty;
        var marked = MarkBookTypes.Readed;


        Assert.Throws<ArgumentException>(() => new MarkBook.Builder()
                     .SetBookId(bookId)
                     .SetUserId(userId)
                     .SetMarked(marked)
                     .Build());
    }

    [Fact]
    public void Entity_Is_Not_Created_When_Marked_Is_None()
    {
        var bookId = Guid.NewGuid();
        var userId = Guid.NewGuid().ToString();
        var marked = MarkBookTypes.None;


        Assert.Throws<ArgumentException>(() => new MarkBook.Builder()
                     .SetBookId(bookId)
                     .SetUserId(userId)
                     .SetMarked(marked)
                     .Build());
    }
}
