using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class SoftwareEntityTypeConfiguration : IEntityTypeConfiguration<Software>
{
    public void Configure(EntityTypeBuilder<Software> builder)
    {
        builder.HasKey(s => s.IdSoftware);
        builder.Property(s => s.IdSoftware).ValueGeneratedOnAdd().UseIdentityColumn();
        builder.Property(s => s.Name).IsRequired().HasMaxLength(200);
        builder.Property(s => s.Description).IsRequired().HasMaxLength(400);
        builder.Property(s => s.Version).IsRequired().HasMaxLength(10);
        builder.Property(s => s.Price).IsRequired().HasColumnType("money");
        builder.HasOne(s => s.Category).WithMany(c => c.Softwares).HasForeignKey(s => s.IdCategory);

        builder.HasData(
            new Software
            {
                IdSoftware = 1, Name = "Visual Studio", Description = "IDE for development", Version = "2022", Price = new decimal(200.22),
                IdCategory = 2
            },
            new Software
                { IdSoftware = 2, Name = "Notepad++", Description = "Text editor", Version = "8.1.9", Price = new decimal(1000.99), IdCategory = 2 },
            new Software
            {
                IdSoftware = 3, Name = "Spotify", Description = "Music streaming", Version = "1.1.72", Price = new decimal(720.59), IdCategory = 3
            });
    }
}