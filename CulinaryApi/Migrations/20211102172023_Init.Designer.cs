﻿// <auto-generated />
using System;
using CulinaryApi.Core.Entieties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CulinaryApi.Migrations
{
    [DbContext(typeof(CulinaryDbContext))]
    [Migration("20211102172023_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CulinaryApi.Core.Entieties.Cuisine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cuisines");
                });

            modelBuilder.Entity("CulinaryApi.Core.Entieties.Difficulty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");
                });

            modelBuilder.Entity("CulinaryApi.Core.Entieties.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("CulinaryApi.Core.Entieties.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CuisineId")
                        .HasColumnType("int");

                    b.Property<int?>("DifficultyId")
                        .HasColumnType("int");

                    b.Property<string>("Execution")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grammar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TimeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CuisineId");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("MealId");

                    b.HasIndex("TimeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("CulinaryApi.Core.Entieties.Time", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TImes");
                });

            modelBuilder.Entity("CulinaryApi.Core.Entieties.Recipe", b =>
                {
                    b.HasOne("CulinaryApi.Core.Entieties.Cuisine", null)
                        .WithMany("Recipes")
                        .HasForeignKey("CuisineId");

                    b.HasOne("CulinaryApi.Core.Entieties.Difficulty", null)
                        .WithMany("Recipes")
                        .HasForeignKey("DifficultyId");

                    b.HasOne("CulinaryApi.Core.Entieties.Meal", "Meal")
                        .WithMany("Recipes")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CulinaryApi.Core.Entieties.Time", null)
                        .WithMany("Recipes")
                        .HasForeignKey("TimeId");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("CulinaryApi.Core.Entieties.Cuisine", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("CulinaryApi.Core.Entieties.Difficulty", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("CulinaryApi.Core.Entieties.Meal", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("CulinaryApi.Core.Entieties.Time", b =>
                {
                    b.Navigation("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}
