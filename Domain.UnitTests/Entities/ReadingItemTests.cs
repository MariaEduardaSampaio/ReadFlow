using Domain.Common.Enums;
using Domain.Entities;
using Domain.Exceptions;
using FluentAssertions;

namespace Domain.UnitTests.Entities;

[TestFixture]
public sealed class ReadingItemTests
{
    [Test]
    public void Constructor_WhenParamsAreDefined_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bookId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var status = EReadingStatus.Reading;
        var createdAt = new DateTime(2025, 01, 10, 10, 00, 00);

        var currentPage = 123;
        var review = "Muito bom, leitura fluida.";
        var rating = 9;
        DateTime? readingStartedAt = new DateTime(2025, 01, 11, 08, 30, 00);
        DateTime? readingFinishedAt = new DateTime(2025, 01, 20, 22, 15, 00);

        // Act
        var readingItem = new ReadingItem(
            id,
            bookId,
            userId,
            status,
            createdAt,
            currentPage,
            review,
            rating,
            readingStartedAt,
            readingFinishedAt
        );

        // Assert
        readingItem.Id.Should().Be(id);
        readingItem.BookId.Should().Be(bookId);
        readingItem.UserId.Should().Be(userId);
        readingItem.Status.Should().Be(status);
        readingItem.CreatedAt.Should().Be(createdAt);
        readingItem.CurrentPage.Should().Be(currentPage);
        readingItem.Review.Text.Should().Be(review);
        readingItem.Rating.Value.Should().Be(rating);
        readingItem.ReadingStartedAt.Should().Be(readingStartedAt);
        readingItem.ReadingFinishedAt.Should().Be(readingFinishedAt);
    }

    [Test]
    public void Constructor_WhenOptionalParamsAreNull_ShouldSetOptionalPropertiesToNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bookId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var status = EReadingStatus.ToRead;
        var createdAt = new DateTime(2025, 01, 10, 10, 00, 00);

        // Act
        var readingItem = new ReadingItem(
            id,
            bookId,
            userId,
            status,
            createdAt,
            currentPage: null,
            review: null,
            rating: null,
            readingStartedAt: null,
            readingFinishedAt: null
        );

        // Assert
        readingItem.Id.Should().Be(id);
        readingItem.BookId.Should().Be(bookId);
        readingItem.UserId.Should().Be(userId);
        readingItem.Status.Should().Be(status);
        readingItem.CreatedAt.Should().Be(createdAt);
        readingItem.Rating.Value.Should().Be(0);

        readingItem.CurrentPage.Should().BeNull();
        readingItem.Review.Text.Should().BeNull();
        readingItem.ReadingStartedAt.Should().BeNull();
        readingItem.ReadingFinishedAt.Should().BeNull();
    }
    
    
    [Test]
    public void Constructor_WhenCurrentPageIsNegative_ShouldThrowRuleViolationDomainException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bookId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var status = EReadingStatus.Reading;
        var createdAt = new DateTime(2025, 01, 10, 10, 00, 00);

        // Act
        var action = () => new ReadingItem(
            id,
            bookId,
            userId,
            status,
            createdAt,
            currentPage: -1,
            review: "ok",
            rating: 5,
            readingStartedAt: null,
            readingFinishedAt: null
        );

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("*Current page cannot be less than 0*");
    }

    [Test]
    public void Constructor_WhenStartDateIsLaterThanEndDate_ShouldThrowRuleViolationDomainException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bookId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var status = EReadingStatus.Reading;
        var createdAt = new DateTime(2025, 01, 10, 10, 00, 00);

        var start = new DateTime(2025, 02, 02);
        var end = new DateTime(2025, 02, 01);

        // Act
        var action = () => new ReadingItem(
            id,
            bookId,
            userId,
            status,
            createdAt,
            currentPage: 0,
            review: "ok",
            rating: 5,
            readingStartedAt: start,
            readingFinishedAt: end
        );

        // Assert
        action.Should()
            .Throw<RuleViolationDomainException>()
            .WithMessage("*Start date*cannot be later than end date*");
    }

    [Test]
    public void Constructor_WhenOnlyStartDateIsProvided_ShouldNotThrow()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bookId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var status = EReadingStatus.Reading;
        var createdAt = new DateTime(2025, 01, 10, 10, 00, 00);

        var start = new DateTime(2025, 02, 01);

        // Act
        var action = () => new ReadingItem(
            id,
            bookId,
            userId,
            status,
            createdAt,
            currentPage: 10,
            review: "ok",
            rating: 5,
            readingStartedAt: start,
            readingFinishedAt: null
        );

        // Assert
        action.Should().NotThrow();
    }

    [Test]
    public void Constructor_WhenOnlyEndDateIsProvided_ShouldNotThrow()
    {
        // Arrange
        var id = Guid.NewGuid();
        var bookId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var status = EReadingStatus.Read;
        var createdAt = new DateTime(2025, 01, 10, 10, 00, 00);

        var end = new DateTime(2025, 02, 01);

        // Act
        var action = () => new ReadingItem(
            id,
            bookId,
            userId,
            status,
            createdAt,
            currentPage: 10,
            review: "ok",
            rating: 5,
            readingStartedAt: null,
            readingFinishedAt: end
        );

        // Assert
        action.Should().NotThrow();
    }

    [Test]
    public void ProtectedParameterlessConstructor_ShouldCreateInstance()
    {
        // Act
        var readingItem = (ReadingItem)Activator.CreateInstance(typeof(ReadingItem), true)!;

        // Assert
        readingItem.Should().NotBeNull();
    }
}
