namespace Domain.Aggregates.Authors;

public class Author
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Bio { get; private set; }

    protected Author() { }

    public Author(Guid id, string name, string? bio)
    {
        Id = id;
        Name = name;
        Bio = bio;
    }
}
