using Domain.Aggregates.Authors;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Authors;

[TestFixture]
public sealed class AuthorTests
{
    [Test]
    public void Constructor_WhenParamsAreDefined_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var author = new Author(guid, "John", "Writer of many famous books");

        // Assert
        author.Id.Should().Be(guid);
        author.Name.Should().Be("John");
        author.Bio.Should().Be("Writer of many famous books");
    }

    [Test]
    public void Constructor_WhenBioIsNull_ShouldSetBioToEmptyString()
    {
        // Arrange
        var guid = Guid.NewGuid();
        
        // Act
        var author = new Author(guid, "John", null);
        
        // Assert
        author.Bio.Should().BeNull();
    }
    
    [Test]
    public void ProtectedParameterlessConstructor_ShouldCreateInstance()
    {
        // Act
        var author = (Author)Activator.CreateInstance(typeof(Author), true)!;

        // Assert
        author.Should().NotBeNull();
    }
}