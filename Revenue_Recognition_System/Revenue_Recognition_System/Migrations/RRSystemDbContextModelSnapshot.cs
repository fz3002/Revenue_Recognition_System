﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Revenue_Recognition_System.Context;

#nullable disable

namespace Revenue_Recognition_System.Migrations
{
    [DbContext(typeof(RRSystemDbContext))]
    partial class RRSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Revenue_Recognition_System.Models.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdClient"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("IdClient");

                    b.ToTable("Clients");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Revenue_Recognition_System.Models.Company", b =>
                {
                    b.HasBaseType("Revenue_Recognition_System.Models.Client");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("KRS")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            IdClient = 1,
                            Address = "123 Main St",
                            Email = "info@companyone.com",
                            PhoneNumber = "123-456-7890",
                            CompanyName = "Company One",
                            KRS = "1234567890"
                        },
                        new
                        {
                            IdClient = 2,
                            Address = "456 Elm St",
                            Email = "info@companytwo.com",
                            PhoneNumber = "098-765-4321",
                            CompanyName = "Company Two",
                            KRS = "0987654321"
                        });
                });

            modelBuilder.Entity("Revenue_Recognition_System.Models.NaturalPerson", b =>
                {
                    b.HasBaseType("Revenue_Recognition_System.Models.Client");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            IdClient = 3,
                            Address = "123 Main St",
                            Email = "john.doe@example.com",
                            PhoneNumber = "123-456-7890",
                            Name = "John",
                            Pesel = "12345678901",
                            Surname = "Doe"
                        },
                        new
                        {
                            IdClient = 4,
                            Address = "456 Elm St",
                            Email = "jane.smith@example.com",
                            PhoneNumber = "098-765-4321",
                            Name = "Jane",
                            Pesel = "09876543210",
                            Surname = "Smith"
                        });
                });

            modelBuilder.Entity("Revenue_Recognition_System.Models.Company", b =>
                {
                    b.HasOne("Revenue_Recognition_System.Models.Client", null)
                        .WithOne()
                        .HasForeignKey("Revenue_Recognition_System.Models.Company", "IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Revenue_Recognition_System.Models.NaturalPerson", b =>
                {
                    b.HasOne("Revenue_Recognition_System.Models.Client", null)
                        .WithOne()
                        .HasForeignKey("Revenue_Recognition_System.Models.NaturalPerson", "IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
