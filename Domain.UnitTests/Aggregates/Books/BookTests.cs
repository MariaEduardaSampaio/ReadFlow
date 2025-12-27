using Domain.Aggregates.Books;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Books;

[TestFixture]
public sealed class BookTests
{
    [Test]
    public void Constructor_WhenParamsAreDefined_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var publishedDate = new DateTime(2020, 05, 10);

        // Act
        var book = new Book(
            guid,
            "Clean Code",
            "A handbook of agile software craftsmanship.",
            "9780132350884",
            464,
            "Prentice Hall",
            publishedDate
        );

        // Assert
        book.Id.Should().Be(guid);
        book.Title.Should().Be("Clean Code");
        book.Synopsis.Should().Be("A handbook of agile software craftsmanship.");
        book.Isbn.Should().Be("9780132350884");
        book.PageCount.Should().Be(464);
        book.Publisher.Should().Be("Prentice Hall");
        book.PublishedDate.Should().Be(publishedDate);
    }

    [Test]
    public void Constructor_WhenIsbnIsNull_ShouldSetIsbnToNull()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var publishedDate = new DateTime(2022, 01, 01);

        // Act
        var book = new Book(
            guid,
            "Domain-Driven Design",
            "Tackling Complexity in the Heart of Software.",
            null,
            560,
            "Addison-Wesley",
            publishedDate
        );

        // Assert
        book.Isbn.Should().BeNull();
    }

    [Test]
    public void ProtectedParameterlessConstructor_ShouldCreateInstance()
    {
        // Act
        var book = (Book)Activator.CreateInstance(typeof(Book), true)!;

        // Assert
        book.Should().NotBeNull();
    }
}