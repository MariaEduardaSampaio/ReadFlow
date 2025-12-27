using Domain.Aggregates.ReadingItems;
using Domain.Aggregates.ReadingItems.ValueObjects;
using Domain.Common.Enums;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.ReadingItems;

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

        int? currentPage = 123;
        var review = "Muito bom, leitura fluida.";
        var rating = Rating.Create(9);
        DateTime? beginningToRead = new DateTime(2025, 01, 11, 08, 30, 00);
        DateTime? endedReading = new DateTime(2025, 01, 20, 22, 15, 00);

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
            beginningToRead,
            endedReading
        );

        // Assert
        readingItem.Id.Should().Be(id);
        readingItem.BookId.Should().Be(bookId);
        readingItem.UserId.Should().Be(userId);
        readingItem.Status.Should().Be(status);
        readingItem.CreatedAt.Should().Be(createdAt);
        readingItem.CurrentPage.Should().Be(currentPage);
        readingItem.Review.Should().Be(review);
        readingItem.Rating.Should().Be(rating);
        readingItem.BeginningToRead.Should().Be(beginningToRead);
        readingItem.EndedReading.Should().Be(endedReading);
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
            beginningToRead: null,
            endedReading: null
        );

        // Assert
        readingItem.Id.Should().Be(id);
        readingItem.BookId.Should().Be(bookId);
        readingItem.UserId.Should().Be(userId);
        readingItem.Status.Should().Be(status);
        readingItem.CreatedAt.Should().Be(createdAt);

        readingItem.CurrentPage.Should().BeNull();
        readingItem.Review.Should().BeNull();
        readingItem.Rating.Should().BeNull();
        readingItem.BeginningToRead.Should().BeNull();
        readingItem.EndedReading.Should().BeNull();
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
