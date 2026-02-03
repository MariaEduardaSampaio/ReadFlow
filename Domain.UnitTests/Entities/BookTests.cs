using Domain.Entities;
using FluentAssertions;

namespace Domain.UnitTests.Entities;

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
        book.Title.Text.Should().Be("Clean Code");
        book.Synopsis.Text.Should().Be("A handbook of agile software craftsmanship.");
        book.Isbn.Value.Should().Be("9780132350884");
        book.PageCount.Should().Be(464);
        book.Publisher.Should().Be("Prentice Hall");
        book.PublishedDate.Should().Be(publishedDate);
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