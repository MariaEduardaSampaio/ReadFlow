using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        
        builder.HasKey(c => c.Id);

        builder.OwnsOne(c => c.Name, e =>
        {
            e.Property(c => c.Text)
                .HasColumnName("category_name")
                .HasColumnType($"varchar({CategoryName.MaxLength})")
                .HasMaxLength(CategoryName.MaxLength)
                .IsRequired();
        });
        
        builder.HasIndex("category_name")
            .IsUnique();
        
        builder.HasMany(c => c.Books)
            .WithMany(b => b.Categories)
            .UsingEntity<Dictionary<string, object>>(
                "book_categories",
                j => j.HasOne<Book>().WithMany().HasForeignKey("book_id").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Category>().WithMany().HasForeignKey("category_id").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("book_categories");
                    j.HasKey("book_id", "category_id");
                }
            );
    }
}