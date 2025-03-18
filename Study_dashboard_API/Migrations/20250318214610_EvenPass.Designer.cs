﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Study_dashboard_API.Data;

#nullable disable

namespace Study_dashboard_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250318214610_EvenPass")]
    partial class EvenPass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Study_dashboard_API.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPassed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("int");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("EventId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventId = 1,
                            Date = new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Nie zdam! xd",
                            IsPassed = false,
                            Name = "sprawdzian z pic",
                            PriorityLevel = 1,
                            SubjectId = 1,
                            UserId = 1
                        },
                        new
                        {
                            EventId = 2,
                            Date = new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "wejsciowka z avr",
                            IsPassed = false,
                            Name = "wejsciowka",
                            PriorityLevel = 1,
                            SubjectId = 2,
                            UserId = 1
                        },
                        new
                        {
                            EventId = 3,
                            Date = new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "literaki",
                            IsPassed = false,
                            Name = "projekt",
                            PriorityLevel = 1,
                            SubjectId = 3,
                            UserId = 1
                        },
                        new
                        {
                            EventId = 4,
                            Date = new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Nie zdam! xd",
                            IsPassed = false,
                            Name = "kolokwium",
                            PriorityLevel = 2,
                            SubjectId = 7,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("Study_dashboard_API.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"));

                    b.Property<int>("Ects")
                        .HasColumnType("int");

                    b.Property<double?>("Grade")
                        .HasColumnType("float");

                    b.Property<bool>("IsPassed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PassingType")
                        .HasColumnType("int");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Subjects");

                    b.HasData(
                        new
                        {
                            SubjectId = 1,
                            Ects = 5,
                            IsPassed = false,
                            Name = "Smiw",
                            PriorityLevel = 1,
                            UserId = 1
                        },
                        new
                        {
                            SubjectId = 2,
                            Ects = 2,
                            IsPassed = false,
                            Name = "Smiw lab",
                            PriorityLevel = 1,
                            UserId = 1
                        },
                        new
                        {
                            SubjectId = 3,
                            Ects = 3,
                            IsPassed = false,
                            Name = "Pk 5",
                            PriorityLevel = 1,
                            UserId = 1
                        },
                        new
                        {
                            SubjectId = 4,
                            Ects = 1,
                            IsPassed = false,
                            Name = "Programowanie 4",
                            PriorityLevel = 1,
                            UserId = 2
                        },
                        new
                        {
                            SubjectId = 5,
                            Ects = 2,
                            IsPassed = false,
                            Name = "PBD",
                            PriorityLevel = 1,
                            UserId = 2
                        },
                        new
                        {
                            SubjectId = 6,
                            Ects = 1,
                            IsPassed = false,
                            Name = "Java_web",
                            PriorityLevel = 1,
                            UserId = 3
                        },
                        new
                        {
                            SubjectId = 7,
                            Ects = 1,
                            IsPassed = false,
                            Name = "Tm",
                            PriorityLevel = 1,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("Study_dashboard_API.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Name = "Kuba1",
                            Password = "kuba1111"
                        },
                        new
                        {
                            UserId = 2,
                            Name = "Kuba2",
                            Password = "kuba2222"
                        },
                        new
                        {
                            UserId = 3,
                            Name = "Kuba3",
                            Password = "kuba3333"
                        },
                        new
                        {
                            UserId = 4,
                            Name = "Kuba4",
                            Password = "kuba4444"
                        },
                        new
                        {
                            UserId = 5,
                            Name = "Kuba5",
                            Password = "kuba5555"
                        },
                        new
                        {
                            UserId = 6,
                            Name = "Kuba6",
                            Password = "kuba6666"
                        });
                });

            modelBuilder.Entity("Study_dashboard_API.Models.Event", b =>
                {
                    b.HasOne("Study_dashboard_API.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Study_dashboard_API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Study_dashboard_API.Models.Subject", b =>
                {
                    b.HasOne("Study_dashboard_API.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
