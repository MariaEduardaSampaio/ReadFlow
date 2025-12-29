using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public sealed class ReviewTests
{
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase("\t")]
    [TestCase("\r\n")]
    [TestCase("Leitura muito boa.")]
    [TestCase("Gostei bastante do desenvolvimento e da escrita.")]
    public void Constructor_WhenReviewIsNullOrValid_ShouldSetText(string? text)
    {
        // Act
        var review = new Review(text);

        // Assert
        review.Text.Should().Be(text);
    }

    [Test]
    public void Constructor_WhenReviewLengthIsGreaterThanMaxLength_ShouldThrowRuleViolationDomainException()
    {
        // Arrange
        var tooLong = new string('a', Review.MaxLength + 1);

        // Act
        var action = () => new Review(tooLong);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage($"The provided review must be smaller or equal to {Review.MaxLength}.");
    }

    [Test]
    public void Constructor_WhenReviewLengthIsExactlyMaxLength_ShouldSetText()
    {
        // Arrange
        var text = new string('a', Review.MaxLength);

        // Act
        var review = new Review(text);

        // Assert
        review.Text.Should().Be(text);
    }

    [Test]
    public void Equality_WhenBothReviewsAreNull_ShouldBeEqual()
    {
        // Arrange
        var a = new Review(null);
        var b = new Review(null);

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenOneIsNullAndOtherIsEmpty_ShouldBeEqual()
    {
        // Arrange
        var a = new Review(null);
        var b = new Review(string.Empty);

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenTextsAreSame_ShouldBeEqual()
    {
        // Arrange
        var a = new Review("Muito bom, leitura fluida.");
        var b = new Review("Muito bom, leitura fluida.");

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenTextsAreDifferent_ShouldNotBeEqual()
    {
        // Arrange
        var a = new Review("Bom.");
        var b = new Review("Ótimo.");

        // Assert
        a.Should().NotBe(b);
    }
}
