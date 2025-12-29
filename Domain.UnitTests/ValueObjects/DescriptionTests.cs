using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public sealed class DescriptionTests
{
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase("Short description")]
    [TestCase("Descrição com acentuação")]
    public void Constructor_WhenDescriptionIsNullOrValid_ShouldSetText(string? description)
    {
        // Act
        var vo = new Description(description);

        // Assert
        vo.Text.Should().Be(description);
    }

    [Test]
    public void Constructor_WhenDescriptionLengthIsGreaterThanMaxLength_ShouldThrowRuleViolationDomainException()
    {
        // Arrange
        var tooLong = new string('a', Description.MaxLength + 1);

        // Act
        var action = () => new Description(tooLong);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage($"The provided description must be smaller or equal to {Description.MaxLength}.");
    }

    [Test]
    public void Constructor_WhenDescriptionLengthIsExactlyMaxLength_ShouldSetText()
    {
        // Arrange
        var text = new string('a', Description.MaxLength);

        // Act
        var vo = new Description(text);

        // Assert
        vo.Text.Should().Be(text);
    }

    [Test]
    public void Equality_WhenBothDescriptionsAreNull_ShouldBeEqual()
    {
        // Arrange
        var a = new Description(null);
        var b = new Description(null);

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenOneIsNullAndOtherIsEmpty_ShouldBeEqual()
    {
        // Arrange
        var a = new Description(null);
        var b = new Description(string.Empty);

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenDescriptionsHaveSameText_ShouldBeEqual()
    {
        // Arrange
        var a = new Description("Writer of many famous books");
        var b = new Description("Writer of many famous books");

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenDescriptionsHaveDifferentText_ShouldNotBeEqual()
    {
        // Arrange
        var a = new Description("Bio A");
        var b = new Description("Bio B");

        // Assert
        a.Should().NotBe(b);
    }
}
