using Application.Abstractions.TransferObjects;
using Domain.Entities;

namespace Application.Abstractions.Mappers;

public static class TransferObjectsMapper
{
    public static ReadingItemDto MapToDto(this ReadingItem readingItem)
    {
        return new ReadingItemDto
        {
            Id = readingItem.Id,
            Status = readingItem.Status,
            CreatedAt = readingItem.CreatedAt,
            Rating = readingItem.Rating.Value,
            Review =  readingItem.Review.Text,
            Book = readingItem.Book.MapToDto(),
            User = readingItem.User.MapToDto(),
            CurrentPage = readingItem.CurrentPage,
            ReadingStartedAt = readingItem.ReadingStartedAt,
            ReadingFinishedAt = readingItem.ReadingFinishedAt
        };
    }
    
    public static UserDto MapToDto(this User user)
    {
        return new UserDto
        {
            Id =  user.Id,
            Role = user.Role.ToString(),
            Name = user.Name.Text ?? string.Empty,
            Email = user.Email.Address,
        };
    }

    private static BookDto MapToDto(this Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title.Text ?? string.Empty,
            Isbn = book.Isbn.Value,
            PageCount = book.PageCount,
            PublishedDate = book.PublishedDate,
            Publisher = book.Publisher,
            Synopsis = book.Synopsis.Text ?? string.Empty,
            Authors = book.Authors.Select(a => a.MapToDto()).ToList(),
            Categories = book.Categories.Select(c => c.MapToDto()).ToList()
        };
    }

    private static AuthorDto MapToDto(this Author author)
    {
        return new AuthorDto
        {
            Id = author.Id,
            Name = author.Description.Text ?? string.Empty,
            Description = author.Description.Text ?? string.Empty
        };
    }
    
    private static CategoryDto MapToDto(this Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name.Text ?? string.Empty
        };
    }
}