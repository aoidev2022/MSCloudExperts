﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OlympicGames.DB;

#nullable disable

namespace OlympicGames.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231103051802_Added_Sample_Entity")]
    partial class Added_Sample_Entity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OlympicGames.DB.Competitor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Competitor");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "COL",
                            FullName = "Anthony Boral"
                        },
                        new
                        {
                            Id = 2,
                            Country = "CHN",
                            FullName = "Marcela Lopez"
                        },
                        new
                        {
                            Id = 3,
                            Country = "AUS",
                            FullName = "Alejandra Ortega"
                        });
                });

            modelBuilder.Entity("OlympicGames.DB.Sample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompetitorId")
                        .HasColumnType("int");

                    b.Property<int>("Mode")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompetitorId");

                    b.ToTable("Samples");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompetitorId = 1,
                            Mode = 1,
                            Score = 134
                        },
                        new
                        {
                            Id = 2,
                            CompetitorId = 1,
                            Mode = 1,
                            Score = 130
                        },
                        new
                        {
                            Id = 3,
                            CompetitorId = 1,
                            Mode = 1,
                            Score = 120
                        },
                        new
                        {
                            Id = 4,
                            CompetitorId = 1,
                            Mode = 2,
                            Score = 177
                        },
                        new
                        {
                            Id = 5,
                            CompetitorId = 1,
                            Mode = 2,
                            Score = 100
                        },
                        new
                        {
                            Id = 6,
                            CompetitorId = 1,
                            Mode = 2,
                            Score = 115
                        },
                        new
                        {
                            Id = 7,
                            CompetitorId = 2,
                            Mode = 1,
                            Score = 130
                        },
                        new
                        {
                            Id = 8,
                            CompetitorId = 2,
                            Mode = 1,
                            Score = 90
                        },
                        new
                        {
                            Id = 9,
                            CompetitorId = 2,
                            Mode = 1,
                            Score = 125
                        },
                        new
                        {
                            Id = 10,
                            CompetitorId = 2,
                            Mode = 2,
                            Score = 180
                        },
                        new
                        {
                            Id = 11,
                            CompetitorId = 2,
                            Mode = 2,
                            Score = 112
                        },
                        new
                        {
                            Id = 12,
                            CompetitorId = 2,
                            Mode = 2,
                            Score = 120
                        },
                        new
                        {
                            Id = 13,
                            CompetitorId = 3,
                            Mode = 1,
                            Score = 0
                        },
                        new
                        {
                            Id = 14,
                            CompetitorId = 3,
                            Mode = 1,
                            Score = 0
                        },
                        new
                        {
                            Id = 15,
                            CompetitorId = 3,
                            Mode = 1,
                            Score = 0
                        },
                        new
                        {
                            Id = 16,
                            CompetitorId = 3,
                            Mode = 2,
                            Score = 150
                        },
                        new
                        {
                            Id = 17,
                            CompetitorId = 3,
                            Mode = 2,
                            Score = 149
                        },
                        new
                        {
                            Id = 18,
                            CompetitorId = 3,
                            Mode = 2,
                            Score = 150
                        });
                });

            modelBuilder.Entity("OlympicGames.DB.Sample", b =>
                {
                    b.HasOne("OlympicGames.DB.Competitor", "Competitor")
                        .WithMany("Samples")
                        .HasForeignKey("CompetitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competitor");
                });

            modelBuilder.Entity("OlympicGames.DB.Competitor", b =>
                {
                    b.Navigation("Samples");
                });
#pragma warning restore 612, 618
        }
    }
}
