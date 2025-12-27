using Domain.Aggregates.Categories;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Categories;

[TestFixture]
public sealed class CategoryTests
{
    [Test]
    public void Constructor_WhenParamsAreDefined_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var category = new Category(guid, "Fantasy");

        // Assert
        category.Id.Should().Be(guid);
        category.Name.Should().Be("Fantasy");
    }

    [Test]
    public void ProtectedParameterlessConstructor_ShouldCreateInstance()
    {
        // Act
        var category = (Category)Activator.CreateInstance(typeof(Category), true)!;

        // Assert
        category.Should().NotBeNull();
    }
}