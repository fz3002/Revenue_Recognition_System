using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class ClientEntityTypeConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.UseTptMappingStrategy();
        builder.HasKey(c => c.IdClient);
        builder.Property(c => c.IdClient).ValueGeneratedOnAdd().UseIdentityColumn();
        builder.Property(c => c.Address).IsRequired().HasMaxLength(300);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(50);
        builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(12);
    }
}