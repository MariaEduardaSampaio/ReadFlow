using Domain.ValueObjects;

namespace Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public CategoryName Name { get; private set; }
    public ICollection<Book> Books { get; private set; } = [];

    protected Category() { }

    public Category(Guid id, string name)
    {
        Id = id;
        Name = new CategoryName(name);
    }
}
