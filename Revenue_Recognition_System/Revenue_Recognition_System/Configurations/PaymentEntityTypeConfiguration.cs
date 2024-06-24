using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.IdPayment);
        builder.Property(p => p.IdPayment).ValueGeneratedOnAdd().UseIdentityColumn();
        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.Value).IsRequired().HasColumnType("money");
        builder.HasOne(p => p.Client).WithMany(c => c.Payments).HasForeignKey(p => p.IdClient).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(p => p.Contract).WithMany(c => c.Payments).HasForeignKey(p => p.IdContract).OnDelete(DeleteBehavior.NoAction);
    }
}