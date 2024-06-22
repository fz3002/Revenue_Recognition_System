using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.UseTptMappingStrategy();
        builder.HasBaseType<Client>();
        builder.Property(c => c.CompanyName).IsRequired().HasMaxLength(300);
        builder.Property(c => c.KRS).IsRequired().HasMaxLength(10);
        builder.HasIndex(c => c.KRS).IsUnique();

        builder.HasData(
            new Company("1234567890")
            {
                IdClient = 1,
                CompanyName = "Company One",
                Address = "123 Main St",
                Email = "info@companyone.com",
                PhoneNumber = "123-456-7890"
            },
            new Company("0987654321")
            {
                IdClient = 2,
                CompanyName = "Company Two",
                Address = "456 Elm St",
                Email = "info@companytwo.com",
                PhoneNumber = "098-765-4321"
            }
        );
    }
}