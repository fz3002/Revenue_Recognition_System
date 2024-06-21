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
    }
}