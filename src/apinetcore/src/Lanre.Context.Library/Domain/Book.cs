namespace Lanre.Context.Library.Domain;

public class Book
{
    private Book()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; internal set; }

    public string Name { get; internal set; }

    public string? Series { get; internal set; }

    public string? Authors { get; internal set; }

    public string? Rating { get; internal set; }

    public string? Sinopsis { get; internal set; }

    public string? ImageUrl { get; internal set; }

    public string? Url { get; internal set; }

    public string? Pages { get; internal set; }

    public string UserId { get; internal set; }

    public Book SetName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }
        Name = name;
        return this;
    }

    public Book SetUserId(string? userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException($"'{nameof(userId)}' cannot be null or whitespace.", nameof(userId));
        }
        UserId = userId;
        return this;
    }

    public Book SetImageUrl(string? imageUrl)
    {
        ImageUrl = !string.IsNullOrWhiteSpace(imageUrl)
                    ? imageUrl
                    : "https://law.ku.edu.tr/wp-content/themes/kuwp/assets/img/sample/book-placeholder.png";
        return this;
    }

    public Book SetSeries(string? series)
    {
        Series = series;
        return this;
    }

    public Book SetAuthors(string? authors)
    {
        Authors = authors;
        return this;
    }

    public Book SetRating(string? rating)
    {
        Rating = rating;
        return this;
    }

    public Book SetSinopsis(string? sinopsis)
    {
        Sinopsis = sinopsis;
        return this;
    }

    public Book SetUrl(string? url)
    {
        Url = url;
        return this;
    }

    public Book SetPages(string? pages)
    {
        Pages = pages;
        return this;
    }

    public class Builder
    {
        private string? name;
        private string? series;
        private string? authors;
        private string? rating;
        private string? sinopsis;
        private string? imageUrl;
        private string? url;
        private string? pages;
        private string? userId;

        public Builder SetName(string? name)
        {
            this.name = name;
            return this;
        }

        public Builder SetSeries(string? series)
        {
            this.series = series;
            return this;
        }
        public Builder SetAuthors(string? authors)
        {
            this.authors = authors;
            return this;
        }
        public Builder SetRating(string? rating)
        {
            this.rating = rating;
            return this;
        }
        public Builder SetSinopsis(string? sinopsis)
        {
            this.sinopsis = sinopsis;
            return this;
        }
        public Builder SetImageUrl(string? imageUrl)
        {
            this.imageUrl = imageUrl;
            return this;
        }
        public Builder SetUrl(string? url)
        {
            this.url = url;
            return this;
        }
        public Builder SetPages(string? pages)
        {
            this.pages = pages;
            return this;
        }
        public Builder SetUserId(string? userId)
        {
            this.userId = userId;
            return this;
        }

        public Book Build()
        {
            var entity = new Book()
                            .SetName(this.name)
                            .SetUserId(this.userId)
                            .SetSeries(this.series)
                            .SetAuthors(this.authors)
                            .SetRating(this.rating)
                            .SetSinopsis(this.sinopsis)
                            .SetImageUrl(this.imageUrl)
                            .SetUrl(this.url)
                            .SetPages(this.pages)
                            ;
            return entity;
        }
    }
}