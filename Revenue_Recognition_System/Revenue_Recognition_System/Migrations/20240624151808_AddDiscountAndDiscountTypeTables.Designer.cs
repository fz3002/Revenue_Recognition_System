﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Revenue_Recognition_System.Context;

#nullable disable

namespace Revenue_Recognition_System.Migrations
{
    [DbContext(typeof(RRSystemDbContext))]
    [Migration("20240624151808_AddDiscountAndDiscountTypeTables")]
    partial class AddDiscountAndDiscountTypeTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Revenue_Recognition_System.Models.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategory"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdCategory");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            IdCategory = 1,
                            Name = "Productivity"
                        },
                        new
                        {
                            IdCategory = 2,
                            Name = "Development"
                        },
                        new
                        {
                            IdCategory = 3,
                            Name = "Entertainment"
                        });
                });

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

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("IdClient");

                    b.ToTable("Clients");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("Revenue_Recognition_System.Models.Discount", b =>
                {
                    b.Property<int>("IdDiscount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDiscount"));

                    b.Property<DateOnly>("DateFrom")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DateTo")
                        .HasColumnType("date");

                    b.Property<int>("IdDiscountType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdDiscount");

                    b.HasIndex("IdDiscountType");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            IdDiscount = 1,
                            DateFrom = new DateOnly(2024, 6, 1),
                            DateTo = new DateOnly(2024, 6, 30),
                            IdDiscountType = 1,
                            Name = "Summer Sale",
                            Value = 50.0m
                        },
                        new
                        {
                            IdDiscount = 2,
                            DateFrom = new DateOnly(2024, 11, 25),
                            DateTo = new DateOnly(2024, 11, 30),
                            IdDiscountType = 1,
                            Name = "Black Friday",
                            Value = 70.0m
                        },
                        new
                        {
                            IdDiscount = 3,
                            DateFrom = new DateOnly(2024, 12, 20),
                            DateTo = new DateOnly(2024, 12, 25),
                            IdDiscountType = 1,
                            Name = "Christmas Sale",
                            Value = 30.0m
                        });
                });

            modelBuilder.Entity("Revenue_Recognition_System.Models.DiscountType", b =>
                {
                    b.Property<int>("IdDiscountType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDiscountType"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("IdDiscountType");

                    b.ToTable("DiscountTypes");

                    b.HasData(
                        new
                        {
                            IdDiscountType = 1,
                            Name = "Discount for one time purchase"
                        },
                        new
                        {
                            IdDiscountType = 2,
                            Name = "Discount for subscription"
                        });
                });

            modelBuilder.Entity("Revenue_Recognition_System.Models.Software", b =>
                {
                    b.Property<int>("IdSoftware")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSoftware"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("IdCategory")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("IdSoftware");

                    b.HasIndex("IdCategory");

                    b.ToTable("Softwares");

                    b.HasData(
                        new
                        {
                            IdSoftware = 1,
                            Description = "IDE for development",
                            IdCategory = 2,
                            Name = "Visual Studio",
                            Price = 200.22m,
                            Version = "2022"
                        },
                        new
                        {
                            IdSoftware = 2,
                            Description = "Text editor",
                            IdCategory = 2,
                            Name = "Notepad++",
                            Price = 1000.99m,
                            Version = "8.1.9"
                        },
                        new
                        {
                            IdSoftware = 3,
                            Description = "Music streaming",
                            IdCategory = 3,
                            Name = "Spotify",
                            Price = 720.59m,
                            Version = "1.1.72"
                        });
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

                    b.HasIndex("KRS")
                        .IsUnique()
                        .HasFilter("[KRS] IS NOT NULL");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            IdClient = 1,
                            Address = "123 Main St",
                            Email = "info@companyone.com",
                            IsDeleted = false,
                            PhoneNumber = "123-456-7890",
                            CompanyName = "Company One",
                            KRS = "1234567890"
                        },
                        new
                        {
                            IdClient = 2,
                            Address = "456 Elm St",
                            Email = "info@companytwo.com",
                            IsDeleted = false,
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

                    b.HasIndex("Pesel")
                        .IsUnique()
                        .HasFilter("[Pesel] IS NOT NULL");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            IdClient = 3,
                            Address = "123 Main St",
                            Email = "john.doe@example.com",
                            IsDeleted = false,
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
                            IsDeleted = false,
                            PhoneNumber = "098-765-4321",
                            Name = "Jane",
                            Pesel = "09876543210",
                            Surname = "Smith"
                        });
                });

            modelBuilder.Entity("Revenue_Recognition_System.Models.Discount", b =>
                {
                    b.HasOne("Revenue_Recognition_System.Models.DiscountType", "Offer")
                        .WithMany("Discounts")
                        .HasForeignKey("IdDiscountType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("Revenue_Recognition_System.Models.Software", b =>
                {
                    b.HasOne("Revenue_Recognition_System.Models.Category", "Category")
                        .WithMany("Softwares")
                        .HasForeignKey("IdCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
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

            modelBuilder.Entity("Revenue_Recognition_System.Models.Category", b =>
                {
                    b.Navigation("Softwares");
                });

            modelBuilder.Entity("Revenue_Recognition_System.Models.DiscountType", b =>
                {
                    b.Navigation("Discounts");
                });
#pragma warning restore 612, 618
        }
    }
}
