using Domain.Common.Enums;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations;

public class ReadingItemConfiguration: IEntityTypeConfiguration<ReadingItem>
{
    public void Configure(EntityTypeBuilder<ReadingItem> builder)
    {
        builder.ToTable("reading_items");
        
        builder.HasKey(r => r.Id);
        
        builder.Property(x => x.BookId)
            .HasColumnName("book_id")
            .IsRequired();
        
        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(r => r.Status)
            .HasColumnName("status")
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<EReadingStatus>(v)
            )
            .IsRequired();
        
        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(r => r.CurrentPage)
            .HasColumnName("current_page")
            .HasColumnType("integer");
        
        builder.OwnsOne(r => r.Review, e =>
        {
            e.Property(r => r.Text)
                .HasColumnName("review")
                .HasColumnType($"varchar({Review.MaxLength})")
                .HasMaxLength(Review.MaxLength);
        });
        
        builder.OwnsOne(r => r.Rating, e =>
        {
            e.Property(r => r.Value)
                .HasColumnName("rating_value");
        });

        builder.Property(r => r.ReadingStartedAt)
            .HasColumnName("reading_started_at")
            .HasColumnType("timestamptz");
        
        builder.Property(r => r.ReadingFinishedAt)
            .HasColumnName("reading_finished_at")
            .HasColumnType("timestamptz");
        
        builder.HasOne(r => r.User)
            .WithMany(u => u.ReadingItems)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
        builder.HasOne(r => r.Book)
            .WithMany(b => b.ReadingItems)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(x => new { x.UserId, x.BookId })
            .IsUnique();
    }
}