using Domain.Common.Enums;

namespace Domain.Aggregates.Users;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public EUserRole Role { get; private set; }

    protected User() { }

    public User(Guid id, string name, string email, EUserRole role)
    {
        Id = id;
        Name = name;
        Email = email;
        Role = role;
    }
}