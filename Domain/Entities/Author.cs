using Domain.ValueObjects;

namespace Domain.Entities;

public class Author
{
    public Guid Id { get; private set; }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public ICollection<Book> Books { get; private set; } = [];

    protected Author() { }

    public Author(Guid id, string name, string? description)
    {
        Id = id;
        Name = new Name(name);
        Description = new Description(description);
    }
}
