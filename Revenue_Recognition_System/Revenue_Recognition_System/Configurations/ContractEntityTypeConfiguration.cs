using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class ContractEntityTypeConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(c => c.IdContract);
        builder.Property(c => c.IdContract)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        builder.Property(c => c.StartDate).IsRequired();
        builder.Property(c => c.EndDate).IsRequired();
        builder.Property(c => c.Paid).IsRequired();
        builder.Property(c => c.YearsOfSupport).IsRequired();
        builder.Property(c => c.Value).IsRequired().HasColumnType("money");
        builder.HasOne(c => c.Discount)
            .WithMany(d => d.Contracts)
            .HasForeignKey(c => c.IdDiscount);
        builder.HasOne(c => c.Client)
            .WithMany(c => c.Contracts)
            .HasForeignKey(c => c.IdClient);
        builder.HasOne(c => c.Software)
            .WithMany(s => s.Contracts)
            .HasForeignKey(c => c.IdSoftware);
    }
}