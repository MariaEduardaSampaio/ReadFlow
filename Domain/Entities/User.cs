using Domain.Common.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public EUserRole Role { get; private set; }
    public ICollection<ReadingItem> ReadingItems { get; private set; } = [];

    protected User() { }

    public User(Guid id, string name, string email, EUserRole role)
    {
        Id = id;
        Name = new Name(name);
        Email = new Email(email);
        Role = role;
    }
}