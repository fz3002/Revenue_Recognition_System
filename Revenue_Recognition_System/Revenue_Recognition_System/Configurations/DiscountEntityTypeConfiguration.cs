using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class DiscountEntityTypeConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(d => d.IdDiscount);
        builder.Property(d => d.IdDiscount).ValueGeneratedOnAdd().UseIdentityColumn();
        builder.Property(d => d.Name).IsRequired().HasMaxLength(200);
        builder.Property(d => d.Value).IsRequired();
        builder.Property(d => d.DateFrom).IsRequired();
        builder.Property(d => d.DateTo).IsRequired();
        builder.HasOne(d => d.Offer).WithMany(dt => dt.Discounts).HasForeignKey(d => d.IdDiscountType);

        builder.HasData(
            new Discount
            {
                IdDiscount = 1, Name = "Summer Sale", IdDiscountType = 1, Value = 0.5m,
                DateFrom = new DateOnly(2024, 6, 1), DateTo = new DateOnly(2024, 6, 30)
            },
            new Discount
            {
                IdDiscount = 2, Name = "Black Friday", IdDiscountType = 1, Value = 0.7m,
                DateFrom = new DateOnly(2024, 11, 25), DateTo = new DateOnly(2024, 11, 30)
            },
            new Discount
            {
                IdDiscount = 3, Name = "Christmas Sale", IdDiscountType = 1, Value = 0.3m,
                DateFrom = new DateOnly(2024, 12, 20), DateTo = new DateOnly(2024, 12, 25)
            }
        );
    }
}