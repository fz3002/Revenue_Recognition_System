using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.IdCategory);
        builder.Property(c => c.IdCategory).ValueGeneratedOnAdd().UseIdentityColumn();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

        builder.HasData
        (
            new Category { IdCategory = 1, Name = "Productivity" },
            new Category { IdCategory = 2, Name = "Development" },
            new Category { IdCategory = 3, Name = "Entertainment" }
            );
    }
}