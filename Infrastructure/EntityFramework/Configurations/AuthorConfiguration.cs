using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations;

public class AuthorConfiguration: IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.ToTable("authors");
        
        builder.HasKey(a => a.Id);

        builder.OwnsOne(a => a.Name, e =>
        {
            e.Property(n => n.Text)
                .HasColumnName("name")
                .HasColumnType($"varchar({Name.MaxLength})")
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });
            
        builder.OwnsOne(a => a.Description, e =>
        {
            e.Property(d => d.Text)
                .HasColumnName("description")
                .HasColumnType($"varchar({Description.MaxLength})")
                .HasMaxLength(Description.MaxLength);
        });
        
        builder.HasMany(a => a.Books)
            .WithMany(b => b.Authors)
            .UsingEntity<Dictionary<string, object>>(
                "book_authors",
                j => j.HasOne<Book>().WithMany().HasForeignKey("book_id").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Author>().WithMany().HasForeignKey("author_id").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.ToTable("book_authors");
                    j.HasKey("book_id", "author_id");
                });
    }
}