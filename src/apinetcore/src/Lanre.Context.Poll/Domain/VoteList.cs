namespace Lanre.Context.Poll.Domain
{
    public class VoteList
    {
        public VoteList()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; internal set; }
        
        public string Name { get; internal set; }

        public string UserId { get; internal set; }


        public VoteList SetName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }
            Name = name;
            return this;
        }

        public VoteList SetUserId(string? userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException($"'{nameof(userId)}' cannot be null or whitespace.", nameof(userId));
            }
            UserId = userId;
            return this;
        }

        public class Builder
        {
            private string? name;
            private string? userId;

            public Builder SetName(string? name)
            {
                this.name = name;
                return this;
            }

            public Builder SetUserId(string? userId)
            {
                this.userId = userId;
                return this;
            }

            public VoteList Build()
            {
                var entity = new VoteList()
                                .SetName(this.name)
                                .SetUserId(this.userId)
                                ;
                return entity;
            }
        }
    }
}
