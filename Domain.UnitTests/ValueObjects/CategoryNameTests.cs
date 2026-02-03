using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public sealed class CategoryNameTests
{
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase("\t")]
    [TestCase("\n")]
    [TestCase("\r\n")]
    public void Constructor_WhenNameIsNullOrWhitespace_ShouldThrowRuleViolationDomainException(string? name)
    {
        // Act
        var action = () => new CategoryName(name);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("The provided category name must not be null or empty.");
    }

    [Test]
    public void Constructor_WhenNameLengthIsGreaterThanMaxLength_ShouldThrowRuleViolationDomainException()
    {
        // Arrange
        var tooLong = new string('a', CategoryName.MaxLength + 1);

        // Act
        var action = () => new CategoryName(tooLong);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage($"The provided category name must be smaller or equal to {CategoryName.MaxLength}.");
    }

    [Test]
    public void Constructor_WhenNameLengthIsExactlyMaxLength_ShouldSetText()
    {
        // Arrange
        var name = new string('a', CategoryName.MaxLength);

        // Act
        var categoryName = new CategoryName(name);

        // Assert
        categoryName.Text.Should().Be(name);
    }

    [TestCase("Fantasy")]
    [TestCase("Sci-Fi")]
    [TestCase("Romance")]
    [TestCase("A")]
    [TestCase("0")]
    public void Constructor_WhenNameIsValid_ShouldSetText(string name)
    {
        // Act
        var categoryName = new CategoryName(name);

        // Assert
        categoryName.Text.Should().Be(name);
    }

    [Test]
    public void Equality_WhenTextIsTheSame_ShouldBeEqual()
    {
        // Arrange
        var a = new CategoryName("Fantasy");
        var b = new CategoryName("Fantasy");

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenTextIsDifferent_ShouldNotBeEqual()
    {
        // Arrange
        var a = new CategoryName("Fantasy");
        var b = new CategoryName("Romance");

        // Assert
        a.Should().NotBe(b);
    }
}
