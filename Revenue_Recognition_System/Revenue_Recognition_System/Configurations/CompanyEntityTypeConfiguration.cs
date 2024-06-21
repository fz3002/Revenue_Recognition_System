using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.IdCompany);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(300);
        builder.Property(c => c.Address).IsRequired().HasMaxLength(300);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(50);
        builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(12);
        builder.Property(c => c.KRS).IsRequired().HasMaxLength(10);

        builder.HasData(
            new Company("1234567890")
            {
                Name = "Company One",
                Address = "123 Main St",
                Email = "info@companyone.com",
                PhoneNumber = "123-456-7890"
            },
            new Company("0987654321")
            {
                Name = "Company Two",
                Address = "456 Elm St",
                Email = "info@companytwo.com",
                PhoneNumber = "098-765-4321"
            }
        );
    }
}