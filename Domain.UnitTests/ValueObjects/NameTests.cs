using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public sealed class NameTests
{
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase("\t")]
    [TestCase("\r\n")]
    public void Constructor_WhenNameIsNullOrWhitespace_ShouldThrowRuleViolationDomainException(string? name)
    {
        // Act
        var action = () => new Name(name!);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("The provided name must not be null or empty.");
    }

    [Test]
    public void Constructor_WhenNameLengthIsGreaterThanMaxLength_ShouldThrowRuleViolationDomainException()
    {
        // Arrange
        var tooLong = new string('a', Name.MaxLength + 1);

        // Act
        var action = () => new Name(tooLong);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage($"The provided name must be smaller or equal to {Name.MaxLength}.");
    }

    [Test]
    public void Constructor_WhenNameLengthIsExactlyMaxLength_ShouldSetText()
    {
        // Arrange
        var name = new string('a', Name.MaxLength);

        // Act
        var vo = new Name(name);

        // Assert
        vo.Text.Should().Be(name);
    }

    [TestCase("Maria")]
    [TestCase("Maria Eduarda")]
    [TestCase("João")]
    [TestCase("Ana-Clara")]
    [TestCase("O'Connor")]
    [TestCase("A")]
    [TestCase("0")]
    public void Constructor_WhenNameIsValid_ShouldSetText(string name)
    {
        // Act
        var vo = new Name(name);

        // Assert
        vo.Text.Should().Be(name);
    }

    [Test]
    public void Equality_WhenTextIsTheSame_ShouldBeEqual()
    {
        // Arrange
        var a = new Name("Maria");
        var b = new Name("Maria");

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenTextIsDifferent_ShouldNotBeEqual()
    {
        // Arrange
        var a = new Name("Maria");
        var b = new Name("Bianca");

        // Assert
        a.Should().NotBe(b);
    }

    [Test]
    public void Equality_WhenNameDiffersOnlyByCasing_ShouldNotBeEqual()
    {
        // Arrange
        var a = new Name("Maria");
        var b = new Name("maria");

        // Assert
        a.Should().NotBe(b);
    }
}
