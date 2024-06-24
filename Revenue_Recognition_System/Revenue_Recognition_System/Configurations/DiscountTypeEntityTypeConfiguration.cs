using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class DiscountTypeEntityTypeConfiguration : IEntityTypeConfiguration<DiscountType>
{
    public void Configure(EntityTypeBuilder<DiscountType> builder)
    {
        builder.HasKey(dt => dt.IdDiscountType);
        builder.Property(dt => dt.IdDiscountType).ValueGeneratedOnAdd().UseIdentityColumn();
        builder.Property(dt => dt.Name).IsRequired().HasMaxLength(200);

        builder.HasData(
            new DiscountType
            {
                IdDiscountType = 1, Name = "Discount for one time purchase"
            },
            new DiscountType
            {
                IdDiscountType = 2, Name = "Discount for subscription"
            }
            );
    }
}