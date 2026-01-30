using Domain.Entities;

namespace Infrastructure.EntityFramework.Seeds;

public static class SeedData
{
    public static (List<Author> Authors, List<Book> Books, List<Category> Categories) Create()
    {
        var authors = new List<Author>
        {
            new(Guid.CreateVersion7(), "Robert C. Martin", "Clean Code author"),
            new(Guid.CreateVersion7(), "Martin Fowler", "Refactoring author"),
            new(Guid.CreateVersion7(), "Eric Evans", "DDD author"),
            new(Guid.CreateVersion7(), "Andrew Hunt", "Pragmatic Programmer author"),
            new(Guid.CreateVersion7(), "David Thomas", "Pragmatic Programmer author")
        };

        var categories = new List<Category>
        {
            new(Guid.NewGuid(), "Software Engineering"),
            new(Guid.NewGuid(), "Programming"),
            new(Guid.NewGuid(), "Architecture"),
            new(Guid.NewGuid(), "Refactoring"),
            new(Guid.NewGuid(), "Best Practices"),

            new(Guid.NewGuid(), "Fantasy"),
            new(Guid.NewGuid(), "Romance"),
            new(Guid.NewGuid(), "Horror"),
            new(Guid.NewGuid(), "Thriller"),
            new(Guid.NewGuid(), "Science Fiction"),
            new(Guid.NewGuid(), "Mystery"),
            new(Guid.NewGuid(), "Adventure")
        };
        
        var books = new List<Book>
        {
            new(Guid.CreateVersion7(), "Clean Code", "A handbook of agile software craftsmanship.", "9780132350884", 464, "Prentice Hall", new DateTime(2008, 8, 1)),
            new(Guid.CreateVersion7(), "Refactoring", "Improving the design of existing code.", "9780201485677", 448, "Addison-Wesley", new DateTime(1999, 7, 8)),
            new(Guid.CreateVersion7(), "Domain-Driven Design", "Tackling Complexity in the Heart of Software.", "9780321125217", 560, "Addison-Wesley", new DateTime(2003, 8, 30)),
            new(Guid.CreateVersion7(), "The Pragmatic Programmer", "Your journey to mastery.", "9780201616224", 352, "Addison-Wesley", new DateTime(1999, 10, 20))
        };

        // Authors relationship
        books[0].Authors.Add(authors[0]);
        books[1].Authors.Add(authors[1]);
        books[2].Authors.Add(authors[2]);
        books[3].Authors.Add(authors[3]);
        books[3].Authors.Add(authors[4]);

        // Categories relationship
        books[0].Categories.Add(categories.Single(c => c.Name.Text == "Programming"));
        books[0].Categories.Add(categories.Single(c => c.Name.Text == "Best Practices"));
        books[0].Categories.Add(categories.Single(c => c.Name.Text == "Software Engineering"));

        books[1].Categories.Add(categories.Single(c => c.Name.Text == "Programming"));
        books[1].Categories.Add(categories.Single(c => c.Name.Text == "Refactoring"));
        books[1].Categories.Add(categories.Single(c => c.Name.Text == "Best Practices"));

        books[2].Categories.Add(categories.Single(c => c.Name.Text == "Architecture"));
        books[2].Categories.Add(categories.Single(c => c.Name.Text == "Software Engineering"));

        books[3].Categories.Add(categories.Single(c => c.Name.Text == "Programming"));
        books[3].Categories.Add(categories.Single(c => c.Name.Text == "Best Practices"));
        books[3].Categories.Add(categories.Single(c => c.Name.Text == "Software Engineering"));

        return (authors, books, categories);
    }
}
