using FluentAssertions;

using Lanre.Context.Poll.Domain;

using System;

using Xunit;

namespace Lanre.Context.Poll.UnitTests.Domain.VoteLists;

public class VoteListTests
{
    [Fact]
    public void Entity_Is_Created_When_All_Fields_Was_Filled()
    {
        string? name = "Votación de Enero de 2022";
        string? userId = Guid.NewGuid().ToString();

        var entity = new VoteList.Builder()
               .SetName(name)
               .SetUserId(userId)
               .Build();

        entity.Should().NotBeNull();
        entity.Name.Should().Be(name);
        entity.UserId.Should().Be(userId);
    }

    [Fact]
    public void Entity_Is_Not_Created_When_Name_Is_Empy()
    {
        string? name = string.Empty;
        string? userId = Guid.NewGuid().ToString();

        Assert.Throws<ArgumentException>(() => new VoteList.Builder()
               .SetName(name)
               .SetUserId(userId)
               .Build());
    }

    [Fact]
    public void Entity_Is_Not_Created_When_UserId_Is_Empy()
    {
        string? name = "El nombre del viento";
        string? userId = string.Empty;

        Assert.Throws<ArgumentException>(() => new VoteList.Builder()
               .SetName(name)
               .SetUserId(userId)
               .Build());
    }

}
