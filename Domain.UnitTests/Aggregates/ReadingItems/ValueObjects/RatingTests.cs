using Domain.Aggregates.ReadingItems.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.ReadingItems.ValueObjects;

[TestFixture]
public sealed class RatingTests
{
    [Test]
    public void Create_WhenValueIsWithinRange_ShouldCreateRatingWithValue()
    {
        // Arrange
        const int value = 7;

        // Act
        var rating = Rating.Create(value);

        // Assert
        rating.Value.Should().Be(value);
    }

    [TestCase(0)]
    [TestCase(10)]
    public void Create_WhenValueIsBoundary_ShouldCreateRating(int value)
    {
        // Act
        var rating = Rating.Create(value);

        // Assert
        rating.Value.Should().Be(value);
    }

    [TestCase(-1)]
    [TestCase(11)]
    public void Create_WhenValueIsOutOfRange_ShouldThrowArgumentOutOfRangeException(int value)
    {
        // Act
        var action = () => Rating.Create(value);

        // Assert
        action.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithParameterName(nameof(value))
            .WithMessage("*must be between 0 and 10.*");
    }

    [Test]
    public void Create_WhenSameValueIsUsed_ShouldCreateEquivalentRecords()
    {
        // Act
        var rating1 = Rating.Create(5);
        var rating2 = Rating.Create(5);

        // Assert
        rating1.Should().Be(rating2);
        rating1.GetHashCode().Should().Be(rating2.GetHashCode());
    }
}