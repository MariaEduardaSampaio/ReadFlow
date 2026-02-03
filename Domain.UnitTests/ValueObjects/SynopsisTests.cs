using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public sealed class SynopsisTests
{
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase("\t")]
    [TestCase("\r\n")]
    [TestCase("A handbook of agile software craftsmanship.")]
    [TestCase("Sinopse curta.")]
    public void Constructor_WhenSynopsisIsNullOrValid_ShouldSetText(string? text)
    {
        // Act
        var synopsis = new Synopsis(text);

        // Assert
        synopsis.Text.Should().Be(text);
    }

    [Test]
    public void Constructor_WhenSynopsisLengthIsGreaterThanMaxLength_ShouldThrowRuleViolationDomainException()
    {
        // Arrange
        var tooLong = new string('a', Synopsis.MaxLength + 1);

        // Act
        var action = () => new Synopsis(tooLong);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage($"The provided synopsis must be smaller or equal to {Synopsis.MaxLength}.");
    }

    [Test]
    public void Constructor_WhenSynopsisLengthIsExactlyMaxLength_ShouldSetText()
    {
        // Arrange
        var text = new string('a', Synopsis.MaxLength);

        // Act
        var synopsis = new Synopsis(text);

        // Assert
        synopsis.Text.Should().Be(text);
    }

    [Test]
    public void Equality_WhenBothAreNull_ShouldBeEqual()
    {
        // Arrange
        var a = new Synopsis(null);
        var b = new Synopsis(null);

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenOneIsNullAndOtherIsEmpty_ShouldBeEqual()
    {
        // Arrange
        var a = new Synopsis(null);
        var b = new Synopsis(string.Empty);

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenTextsAreSame_ShouldBeEqual()
    {
        // Arrange
        var a = new Synopsis("Sinopse exemplo.");
        var b = new Synopsis("Sinopse exemplo.");

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenTextsAreDifferent_ShouldNotBeEqual()
    {
        // Arrange
        var a = new Synopsis("Sinopse A");
        var b = new Synopsis("Sinopse B");

        // Assert
        a.Should().NotBe(b);
    }
}
