using FluentAssertions;

using Lanre.Context.Library.Domain;

using System;

using Xunit;

namespace Lanre.Context.Library.UnitTests.Domain.Books;

public class BookTests
{
    [Fact]
    public void Entity_Is_Created_When_All_Fields_Was_Filled()
    {
        string? name = "El nombre del viento";
        string? userId = Guid.NewGuid().ToString();
        string? series = "El asesino de reyes";
        string? authors = "Patrick Rothfus";
        string? rating = "5";
        string? sinopsis = "sinopsis";
        string? imageUrl = "http://google.es";
        string? url = "http://google.es";
        string? pages = "XXX";

        var entity = new Book.Builder()
               .SetName(name)
               .SetUserId(userId)
               .SetAuthors(authors)
               .SetImageUrl(imageUrl)
               .SetUrl(url)
               .SetSeries(series)
               .SetSinopsis(sinopsis)
               .SetPages(pages)
               .SetRating(rating)
               .Build();

        entity.Should().NotBeNull();
        entity.Name.Should().Be(name);
        entity.UserId.Should().Be(userId);
        entity.Series.Should().Be(series);
        entity.Authors.Should().Be(authors);
        entity.Rating.Should().Be(rating);
        entity.Sinopsis.Should().Be(sinopsis);
        entity.ImageUrl.Should().Be(imageUrl);
        entity.Url.Should().Be(url);
        entity.Pages.Should().Be(pages);
    }

    [Fact]
    public void Entity_Is_Created_When_None_Required_Fields_Was_Empty()
    {
        string? name = "El nombre del viento";
        string? userId = Guid.NewGuid().ToString();
        string? series = string.Empty;
        string? authors = string.Empty;
        string? rating = string.Empty;
        string? sinopsis = string.Empty;
        string? imageUrl = string.Empty;
        string? url = string.Empty;
        string? pages = string.Empty;

        var entity = new Book.Builder()
               .SetName(name)
               .SetUserId(userId)
               .SetAuthors(authors)
               .SetImageUrl(imageUrl)
               .SetUrl(url)
               .SetSeries(series)
               .SetSinopsis(sinopsis)
               .SetPages(pages)
               .SetRating(rating)
               .Build();

        entity.Should().NotBeNull();
        entity.Name.Should().Be(name);
        entity.UserId.Should().Be(userId);
        entity.Series.Should().Be(series);
        entity.Authors.Should().Be(authors);
        entity.Rating.Should().Be(rating);
        entity.Sinopsis.Should().Be(sinopsis);
        entity.ImageUrl.Should().NotBe(imageUrl);
        entity.Url.Should().Be(url);
        entity.Pages.Should().Be(pages);
    }

    [Fact]
    public void Entity_Is_Not_Created_When_Name_Is_Empy()
    {
        string? name = string.Empty;
        string? userId = Guid.NewGuid().ToString();
        string? series = "El asesino de reyes";
        string? authors = "Patrick Rothfus";
        string? rating = "5";
        string? sinopsis = "sinopsis";
        string? imageUrl = "http://google.es";
        string? url = "http://google.es";
        string? pages = "XXX";

        Assert.Throws<ArgumentException>(() => new Book.Builder()
               .SetName(name)
               .SetUserId(userId)
               .SetAuthors(authors)
               .SetImageUrl(imageUrl)
               .SetUrl(url)
               .SetSeries(series)
               .SetSinopsis(sinopsis)
               .SetPages(pages)
               .SetRating(rating)
               .Build());
    }

    [Fact]
    public void Entity_Is_Not_Created_When_UserId_Is_Empy()
    {
        string? name = "El nombre del viento";
        string? userId = string.Empty;
        string? series = "El asesino de reyes";
        string? authors = "Patrick Rothfus";
        string? rating = "5";
        string? sinopsis = "sinopsis";
        string? imageUrl = "http://google.es";
        string? url = "http://google.es";
        string? pages = "XXX";

        Assert.Throws<ArgumentException>(() => new Book.Builder()
               .SetName(name)
               .SetUserId(userId)
               .SetAuthors(authors)
               .SetImageUrl(imageUrl)
               .SetUrl(url)
               .SetSeries(series)
               .SetSinopsis(sinopsis)
               .SetPages(pages)
               .SetRating(rating)
               .Build());
    }

}
