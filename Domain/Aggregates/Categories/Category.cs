namespace Domain.Aggregates.Categories;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    protected Category() { }

    public Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
