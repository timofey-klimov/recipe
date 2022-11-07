﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recipes.Persistance;

#nullable disable

namespace Recipes.Persistance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Recipes.Domain.Entities.CookingStage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeCardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeCardId");

                    b.ToTable("CookingStages", (string)null);
                });

            modelBuilder.Entity("Recipes.Domain.Entities.FavouriteRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Dislike")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LikeDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(0)")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("LikedBy")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LikedBy");

                    b.ToTable("FavouriteRecipes", (string)null);
                });

            modelBuilder.Entity("Recipes.Domain.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecipeCardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeCardId");

                    b.ToTable("Ingredients", (string)null);
                });

            modelBuilder.Entity("Recipes.Domain.Entities.RecipeCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(0)")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("ImageSource")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("MealType")
                        .HasColumnType("tinyint");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RecipeCards", (string)null);
                });

            modelBuilder.Entity("Recipes.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(0)")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Salt");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Recipes.Domain.Entities.CookingStage", b =>
                {
                    b.HasOne("Recipes.Domain.Entities.RecipeCard", null)
                        .WithMany("Stages")
                        .HasForeignKey("RecipeCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Recipes.Domain.Entities.FavouriteRecipe", b =>
                {
                    b.HasOne("Recipes.Domain.Entities.User", null)
                        .WithMany("FavouriteRecipes")
                        .HasForeignKey("LikedBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Recipes.Domain.Entities.Ingredient", b =>
                {
                    b.HasOne("Recipes.Domain.Entities.RecipeCard", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Recipes.Domain.Entities.RecipeCard", b =>
                {
                    b.OwnsMany("Recipes.Domain.ValueObjects.Hashtag", "Hashtags", b1 =>
                        {
                            b1.Property<int>("RecipeCardId")
                                .HasColumnType("int")
                                .HasColumnName("RecipeCardId");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"), 1L, 1);

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("RecipeCardId", "Id");

                            b1.ToTable("Hashtags", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RecipeCardId");
                        });

                    b.Navigation("Hashtags");
                });

            modelBuilder.Entity("Recipes.Domain.Entities.RecipeCard", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Stages");
                });

            modelBuilder.Entity("Recipes.Domain.Entities.User", b =>
                {
                    b.Navigation("FavouriteRecipes");
                });
#pragma warning restore 612, 618
        }
    }
}
