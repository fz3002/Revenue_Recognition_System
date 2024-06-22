using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class NaturalPersonEntityTypeConfiguration : IEntityTypeConfiguration<NaturalPerson>
{
    public void Configure(EntityTypeBuilder<NaturalPerson> builder)
    {
        builder.UseTptMappingStrategy();
        builder.HasBaseType<Client>();
        builder.Property(np => np.Name).IsRequired().HasMaxLength(100);
        builder.Property(np => np.Surname).IsRequired().HasMaxLength(200);
        builder.Property(np => np.Pesel).IsRequired().HasMaxLength(11);
        builder.HasIndex(np => np.Pesel).IsUnique();

        builder.HasData(
            new NaturalPerson("12345678901")
            {
                IdClient = 3,
                Name = "John",
                Surname = "Doe",
                Address = "123 Main St",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890"
            },
            new NaturalPerson("09876543210")
            {
                IdClient = 4,
                Name = "Jane",
                Surname = "Smith",
                Address = "456 Elm St",
                Email = "jane.smith@example.com",
                PhoneNumber = "098-765-4321"
            }
        );
    }
}