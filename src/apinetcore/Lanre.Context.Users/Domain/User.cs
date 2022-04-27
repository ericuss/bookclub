using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanre.Context.Users.Domain;


public class User
{
    private User()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; internal set; }

    public string Name { get; private set; }

    public User SetName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }
        Name = name;
        return this;
    }

    internal static User Create(Guid id, string? name)
    {
        var entity = Create(name);
        entity.Id = id;
        return entity;
    }
    public static User Create(string? name)
    {
        var entity = new User();

        return entity.SetName(name);
    }
}
