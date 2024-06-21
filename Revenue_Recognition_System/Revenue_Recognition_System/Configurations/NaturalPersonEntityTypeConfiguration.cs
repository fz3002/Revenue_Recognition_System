using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class NaturalPersonEntityTypeConfiguration : IEntityTypeConfiguration<NaturalPerson>
{
    public void Configure(EntityTypeBuilder<NaturalPerson> builder)
    {
        builder.HasKey(np => np.IdPerson);
        builder.Property(np => np.Name).IsRequired().HasMaxLength(100);
        builder.Property(np => np.Surname).IsRequired().HasMaxLength(200);
        builder.Property(np => np.Address).IsRequired().HasMaxLength(300);
        builder.Property(np => np.PhoneNumber).IsRequired().HasMaxLength(12);
        builder.Property(np => np.Email).IsRequired().HasMaxLength(50);
        builder.Property(np => np.Pesel).IsRequired().HasMaxLength(11);
    }
}