using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations;

public class BookConfiguration: IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("books");
        
        builder.HasKey(r => r.Id);

        builder.OwnsOne(r => r.Title, e =>
        {
            e.Property(r => r.Text)
                .HasColumnName("title")
                .HasColumnType($"varchar({Title.MaxLength})")
                .HasMaxLength(Title.MaxLength)
                .IsRequired();
        });
        
        builder.OwnsOne(r => r.Synopsis, e =>
        {
            e.Property(r => r.Text)
                .HasColumnName("synopsis")
                .HasColumnType($"varchar({Synopsis.MaxLength})")
                .HasMaxLength(Synopsis.MaxLength)
                .IsRequired();
        });
        
        builder.OwnsOne(r => r.Isbn, e =>
        {
            e.Property(r => r.Value)
                .HasColumnName("isbn")
                .HasColumnType("varchar(13)")
                .HasMaxLength(13);
        });
        
        builder.Property(b => b.PageCount)
            .HasColumnName("page_count")
            .HasColumnType("integer")
            .IsRequired();
        
        builder.Property(b => b.PublishedDate)
            .HasColumnName("published_date")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(b => b.Publisher)
            .HasColumnName("publisher")
            .HasColumnType("varchar(200)")
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(r => r.Authors)
            .WithMany(b => b.Books)
            .UsingEntity<Dictionary<string, object>>(
                "book_authors",
                j => j.HasOne<Author>().WithMany().HasForeignKey("author_id").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Book>().WithMany().HasForeignKey("book_id").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("book_authors");
                    j.HasKey("book_id", "author_id");
                });
        
        builder.HasMany(r => r.Categories)
            .WithMany(b => b.Books)
            .UsingEntity<Dictionary<string, object>>(
                "book_categories",
                j => j.HasOne<Category>().WithMany().HasForeignKey("category_id").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Book>().WithMany().HasForeignKey("book_id").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("book_categories");
                    j.HasKey("book_id", "category_id");
                });
        
        builder.HasMany(b => b.ReadingItems)
            .WithOne(ri => ri.Book)
            .HasForeignKey(ri => ri.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}