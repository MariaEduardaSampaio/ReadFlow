using Domain.Exceptions;
using Domain.ValueObjects;
using FluentAssertions;

namespace Domain.UnitTests.ValueObjects;

[TestFixture]
public sealed class EmailTests
{
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("   ")]
    [TestCase("\t")]
    [TestCase("\r\n")]
    public void Constructor_WhenAddressIsNullOrWhitespace_ShouldThrowRuleViolationDomainException(string? address)
    {
        // Act
        var action = () => new Email(address!);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("The email address cannot be empty.");
    }

    [TestCase("test@example.com")]
    [TestCase("user.name+tag@example.com")]
    [TestCase("USER_NAME@example.com")]
    [TestCase("a@b.co")]
    [TestCase("customer/department=shipping@example.com")]
    [TestCase("!#$%&'*+-/=?^_`{|}~@example.com")]
    public void Constructor_WhenAddressIsValid_ShouldNormalizeToLowerAndSetAddress(string address)
    {
        // Act
        var email = new Email(address);

        // Assert
        email.Address.Should().Be(address.Trim().ToLowerInvariant());
    }

    [TestCase(" test@example.com ")]
    [TestCase("  USER@Example.Com  ")]
    public void Constructor_WhenAddressHasLeadingOrTrailingSpaces_ShouldTrimAndNormalize(string address)
    {
        // Act
        var email = new Email(address);

        // Assert
        email.Address.Should().Be(address.Trim().ToLowerInvariant());
    }

    [Test]
    public void Constructor_WhenAddressLengthIsGreaterThanMaxLength_ShouldThrowRuleViolationDomainException()
    {
        // Arrange
        var local = new string('a', 64);
        var domainExtra = new string('b', Email.MaxLength);
        var address = $"{local}@{domainExtra}.com";

        // Act
        var action = () => new Email(address);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage($"The email address must be smaller or equal to {Email.MaxLength} characters.");
    }

    [TestCase("plainaddress")]
    [TestCase("missingatsign.com")]
    [TestCase("a@b")]
    [TestCase("a@b.")]
    [TestCase("a@.com")]
    [TestCase("@example.com")]
    [TestCase("a@@example.com")]
    [TestCase("a b@example.com")]
    [TestCase("a@exa mple.com")]
    [TestCase("a@example..com")]
    [TestCase("a@example.com.")]
    public void Constructor_WhenAddressIsInvalidFormat_ShouldThrowRuleViolationDomainException(string address)
    {
        // Act
        var action = () => new Email(address);

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("The email address is not in a valid format.");
    }

    [Test]
    public void Equality_WhenSameEmailWithDifferentCasing_ShouldBeEqual()
    {
        // Arrange
        var a = new Email("Test@Example.com");
        var b = new Email("test@example.com");

        // Assert
        a.Should().Be(b);
        a.GetHashCode().Should().Be(b.GetHashCode());
    }

    [Test]
    public void Equality_WhenEmailsAreDifferent_ShouldNotBeEqual()
    {
        // Arrange
        var a = new Email("a@example.com");
        var b = new Email("b@example.com");

        // Assert
        a.Should().NotBe(b);
    }

    [Test]
    public void ToString_ShouldReturnNormalizedAddress()
    {
        // Arrange
        var email = new Email("  TeSt@Example.Com  ");

        // Act
        var text = email.ToString();

        // Assert
        text.Should().Be("test@example.com");
    }
}
