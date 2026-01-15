using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Role)
            .HasColumnName("role")
            .HasConversion(
                v => v.ToString(),
                v => Enum.Parse<EUserRole>(v)
            )
            .IsRequired();
        
        builder.OwnsOne(r => r.Name, e =>
        {
            e.Property(r => r.Text)
                .HasColumnName("name")
                .HasColumnType($"varchar({Name.MaxLength})")
                .HasMaxLength(Name.MaxLength)
                .IsRequired();
        });
        
        builder.OwnsOne(r => r.Email, e =>
        {
            e.Property(r => r.Address)
                .HasColumnName("email_address")
                .HasColumnType($"varchar({Email.MaxLength})")
                .HasMaxLength(Email.MaxLength)
                .IsRequired();

            e.HasIndex(u => u.Address)
                .IsUnique();
        });
        
        builder.HasMany(u => u.ReadingItems)
            .WithOne(ri => ri.User)
            .HasForeignKey(ri => ri.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}