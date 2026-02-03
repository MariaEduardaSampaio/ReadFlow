using FluentAssertions;
using Domain.Extensions;

namespace Domain.UnitTests.Extensions;

[TestFixture]
public sealed class StringExtensionsTests
{
    [TestCase("a")]
    [TestCase(" a ")]
    [TestCase("0")]
    public void HasText_WhenValueHasNonWhitespaceCharacters_ShouldBeTrue(string value)
    {
        // Act
        var result = value.HasText;

        // Assert
        result.Should().BeTrue();
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void HasText_WhenValueIsNullOrWhitespace_ShouldBeFalse(string? value)
    {
        // Act
        var result = value.HasText;

        // Assert
        result.Should().BeFalse();
    }

    [TestCase("a")]
    [TestCase(" a ")]
    [TestCase("0")]
    public void IsEmpty_WhenValueHasNonWhitespaceCharacters_ShouldBeFalse(string value)
    {
        // Act
        var result = value.IsEmpty;

        // Assert
        result.Should().BeFalse();
    }
    
    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void IsEmpty_WhenValueIsNullOrWhitespace_ShouldBeTrue(string? value)
    {
        // Act
        var result = value.IsEmpty;

        // Assert
        result.Should().BeTrue();
    }
}