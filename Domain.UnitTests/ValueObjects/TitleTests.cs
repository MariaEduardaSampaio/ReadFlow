using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public sealed class TitleTests
{
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase("\t")]
    [TestCase("\r\n")]
    [TestCase("Clean Code")]
    [TestCase("O Senhor dos Anéis")]
    public void Constructor_WhenTitleIsNullOrValid_ShouldSetText(string? title)
    {
        // Act
        var vo = new Title(title);

        // Assert
        vo.Text.Should().Be(title);
    }

    [Test]
    public void Constructor_WhenTitleLengthIsGreaterThanMaxLength_ShouldThrowRuleViolationDomainException()
    {
        // Arrange
        var tooLong = new string('a', Title.MaxLength + 1);

        // Act
        var action = () => new Title(tooLong);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage($"The provided title must be smaller or equal to {Title.MaxLength}.");
    }

    [Test]
    public void Constructor_WhenTitleLengthIsExactlyMaxLength_ShouldSetText()
    {
        // Arrange
        var title = new string('a', Title.MaxLength);

        // Act
        var vo = new Title(title);

        // Assert
        vo.Text.Should().Be(title);
    }

    [Test]
    public void Equality_WhenBothAreNull_ShouldBeEqual()
    {
        // Arrange
        var a = new Title(null);
        var b = new Title(null);

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenOneIsNullAndOtherIsEmpty_ShouldBeEqual()
    {
        // Arrange
        var a = new Title(null);
        var b = new Title(string.Empty);

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenTextsAreSame_ShouldBeEqual()
    {
        // Arrange
        var a = new Title("Clean Code");
        var b = new Title("Clean Code");

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenTextsAreDifferent_ShouldNotBeEqual()
    {
        // Arrange
        var a = new Title("Clean Code");
        var b = new Title("Refactoring");

        // Assert
        a.Should().NotBe(b);
    }
}
