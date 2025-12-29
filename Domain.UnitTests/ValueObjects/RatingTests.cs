using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public sealed class RatingTests
{
    [Test]
    public void Create_WhenValueIsWithinRange_ShouldCreateRatingWithValue()
    {
        // Arrange
        const int value = 7;

        // Act
        var rating = new Rating(value);

        // Assert
        rating.Value.Should().Be(value);
    }

    [TestCase(0)]
    [TestCase(Rating.MaxValue)]
    public void Create_WhenValueIsBoundary_ShouldCreateRating(int value)
    {
        // Act
        var rating = new Rating(value);

        // Assert
        rating.Value.Should().Be(value);
    }

    [TestCase(-1)]
    [TestCase(11)]
    public void Create_WhenValueIsOutOfRange_ShouldThrowArgumentOutOfRangeException(int value)
    {
        // Act
        var action = () => new Rating(value);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("The provided value must be greater than or equal to 0 and smaller or equal to 10.");
    }

    [Test]
    public void Create_WhenSameValueIsUsed_ShouldCreateEquivalentRecords()
    {
        // Act
        var rating1 = new Rating(5);
        var rating2 = new Rating(5);

        // Assert
        rating1.Should().Be(rating2);
        rating1.GetHashCode().Should().Be(rating2.GetHashCode());
    }
}