using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public sealed class IsbnTests
{
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase("\t")]
    [TestCase("\r\n")]
    public void Constructor_WhenRawIsEmpty_ShouldThrowRuleViolationDomainException(string? raw)
    {
        // Act
        var action = () => new Isbn(raw);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("ISBN cannot be empty.");
    }

    [TestCase("123456789")] 
    [TestCase("12345678901")]
    [TestCase("123456789012")]
    [TestCase("12345678901234")]
    [TestCase("  123456789  ")]
    public void Constructor_WhenNormalizedLengthIsNot10Or13_ShouldThrowRuleViolationDomainException(string raw)
    {
        // Act
        var action = () => new Isbn(raw);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("ISBN must have 10 or 13 characters (after normalization).");
    }

    [TestCase("12345678X9")]
    [TestCase("123456789_")]
    [TestCase("12345A7890")]
    [TestCase("123456789?")]
    public void Constructor_WhenIsbn10HasInvalidFormat_ShouldThrowRuleViolationDomainException(string raw)
    {
        // Act
        var action = () => new Isbn(raw);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("ISBN-10 format is invalid.");
    }


    [TestCase("1234567890")]
    [TestCase("0306406153")]
    [TestCase("0471958698")]
    public void Constructor_WhenIsbn10ChecksumIsInvalid_ShouldThrowRuleViolationDomainException(string raw)
    {
        // Act
        var action = () => new Isbn(raw);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("ISBN-10 checksum is invalid.");
    }

    [TestCase("0306406152")]
    [TestCase("0471958697")]
    [TestCase("156881111X")]
    [TestCase("0-306-40615-2")]
    [TestCase(" 0 306 40615 2 ")]
    public void Constructor_WhenIsbn10IsValid_ShouldSetNormalizedValue(string raw)
    {
        // Act
        var isbn = new Isbn(raw);

        // Assert
        isbn.Value.Should().Be(raw.Trim().Replace("-", "").Replace(" ", "").ToUpperInvariant());
        isbn.Value.Length.Should().Be(10);
    }

    [TestCase("978013235088X")]
    [TestCase("97801323508A4")]
    [TestCase("9780132350_84")]
    public void Constructor_WhenIsbn13HasInvalidFormat_ShouldThrowRuleViolationDomainException(string raw)
    {
        var action = () => new Isbn(raw);

        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("ISBN-13 format is invalid.");
    }


    [TestCase("9780132350885")]
    [TestCase("9780306406158")]
    public void Constructor_WhenIsbn13ChecksumIsInvalid_ShouldThrowRuleViolationDomainException(string raw)
    {
        // Act
        var action = () => new Isbn(raw);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("ISBN-13 checksum is invalid.");
    }

    [TestCase("9780132350884")]
    [TestCase("9780306406157")]
    [TestCase("978-0-13-235088-4")]
    [TestCase(" 978 0 13 235088 4 ")]
    public void Constructor_WhenIsbn13IsValid_ShouldSetNormalizedValue(string raw)
    {
        // Act
        var isbn = new Isbn(raw);

        // Assert
        isbn.Value.Should().Be(raw.Trim().Replace("-", "").Replace(" ", "").ToUpperInvariant());
        isbn.Value.Length.Should().Be(13);
    }

    [Test]
    public void Equality_WhenSameValueDifferentFormatting_ShouldBeEqual()
    {
        // Arrange
        var a = new Isbn("978-0-13-235088-4");
        var b = new Isbn("9780132350884");

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenDifferentValues_ShouldNotBeEqual()
    {
        // Arrange
        var a = new Isbn("9780132350884");
        var b = new Isbn("9780306406157");

        // Assert
        a.Should().NotBe(b);
    }

    [Test]
    public void ToString_ShouldReturnNormalizedValue()
    {
        // Arrange
        var isbn = new Isbn(" 978-0-13-235088-4 ");

        // Act
        var text = isbn.ToString();

        // Assert
        text.Should().Be("9780132350884");
    }
}
