using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revenue_Recognition_System.Enums;
using Revenue_Recognition_System.Helpers;
using Revenue_Recognition_System.Models;

namespace Revenue_Recognition_System.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.IdUser);
        builder.Property(u => u.IdUser).ValueGeneratedOnAdd().UseIdentityColumn();
        builder.Property(u => u.Login).IsRequired();
        builder.Property(u => u.Role).IsRequired();
        builder.Property(u => u.Password).IsRequired();
        builder.Property(u => u.RefreshToken).IsRequired();
        builder.Property(u => u.Salt).IsRequired();
        builder.Property(u => u.RefreshTokenExp).IsRequired();

        var passwordAndSalt = AuthorizationHelpers.GetHashedPasswordAndSalt("admin123");

        builder.HasData(new User
        {
            IdUser = 1,
            Login = "admin",
            Role = Role.Admin,
            Password = passwordAndSalt.Item1,
            Salt = passwordAndSalt.Item2,
            RefreshToken = AuthorizationHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(2)
        });
    }
}