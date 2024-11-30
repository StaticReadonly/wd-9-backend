﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication1.DbConn;

#nullable disable

namespace WebApplication1.DbConn.Migrations
{
    [DbContext(typeof(DbContext1))]
    [Migration("20241130131922_update1")]
    partial class update1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Models.Entities.Comment", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<Guid>("Recipe_ID")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp");

                    b.Property<Guid>("User_ID")
                        .HasColumnType("uuid");

                    b.HasKey("ID");

                    b.HasIndex("Recipe_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Component", b =>
                {
                    b.Property<Guid>("Recipe_ID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Ingredient_ID")
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("numeric(10, 3)");

                    b.HasKey("Recipe_ID", "Ingredient_ID");

                    b.HasIndex("Ingredient_ID");

                    b.ToTable("Components", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Cooking", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<Guid>("Recipe_ID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Step_ID")
                        .HasColumnType("uuid");

                    b.Property<int>("Step_Number")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("Step_ID")
                        .IsUnique();

                    b.HasIndex("Recipe_ID", "Step_ID", "Step_Number")
                        .IsUnique();

                    b.ToTable("Cooking", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Cooking_Material", b =>
                {
                    b.Property<Guid>("Cooking_ID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Material_ID")
                        .HasColumnType("uuid");

                    b.HasKey("Cooking_ID", "Material_ID");

                    b.HasIndex("Material_ID");

                    b.ToTable("Cookings_Materials", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Dish", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<Guid>("Recipe_ID")
                        .HasColumnType("uuid");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Recipe_ID")
                        .IsUnique();

                    b.ToTable("Dishes", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Dish_Tag", b =>
                {
                    b.Property<Guid>("Dish_ID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Tag_ID")
                        .HasColumnType("uuid");

                    b.HasKey("Dish_ID", "Tag_ID");

                    b.HasIndex("Tag_ID");

                    b.ToTable("Dishes_Tags", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Ingredient", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Ingredients", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Material", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.ToTable("Materials", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Menu", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Menus", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Menu_Dish", b =>
                {
                    b.Property<Guid>("Dish_ID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Menu_ID")
                        .HasColumnType("uuid");

                    b.HasKey("Dish_ID", "Menu_ID");

                    b.HasIndex("Menu_ID");

                    b.ToTable("Menus_Dishes", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Recipe", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("Time");

                    b.HasKey("ID");

                    b.ToTable("Recipes", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Step", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.ToTable("Steps", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Tag", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("Last_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar");

                    b.HasKey("ID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Comment", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Recipe", "Recipe")
                        .WithMany("Comments")
                        .HasForeignKey("Recipe_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Entities.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Component", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Ingredient", "Ingredient")
                        .WithMany("RecipeRel")
                        .HasForeignKey("Ingredient_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Entities.Recipe", "Recipe")
                        .WithMany("IngredientRel")
                        .HasForeignKey("Recipe_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Cooking", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Recipe", "Recipe")
                        .WithMany("StepRel")
                        .HasForeignKey("Recipe_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Entities.Step", "Step")
                        .WithOne("Cooking")
                        .HasForeignKey("WebApplication1.Models.Entities.Cooking", "Step_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("Step");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Cooking_Material", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Cooking", "Cooking")
                        .WithMany("MaterialRel")
                        .HasForeignKey("Cooking_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Entities.Material", "Material")
                        .WithMany("CookingRel")
                        .HasForeignKey("Material_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cooking");

                    b.Navigation("Material");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Dish", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Recipe", "Recipe")
                        .WithOne("Dish")
                        .HasForeignKey("WebApplication1.Models.Entities.Dish", "Recipe_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Dish_Tag", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Dish", "Dish")
                        .WithMany("TagsRel")
                        .HasForeignKey("Dish_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Entities.Tag", "Tag")
                        .WithMany("DishesRel")
                        .HasForeignKey("Tag_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Menu_Dish", b =>
                {
                    b.HasOne("WebApplication1.Models.Entities.Dish", "Dish")
                        .WithMany("MenusRel")
                        .HasForeignKey("Dish_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Models.Entities.Menu", "Menu")
                        .WithMany("DishesRel")
                        .HasForeignKey("Menu_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Cooking", b =>
                {
                    b.Navigation("MaterialRel");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Dish", b =>
                {
                    b.Navigation("MenusRel");

                    b.Navigation("TagsRel");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Ingredient", b =>
                {
                    b.Navigation("RecipeRel");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Material", b =>
                {
                    b.Navigation("CookingRel");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Menu", b =>
                {
                    b.Navigation("DishesRel");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Recipe", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Dish")
                        .IsRequired();

                    b.Navigation("IngredientRel");

                    b.Navigation("StepRel");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Step", b =>
                {
                    b.Navigation("Cooking")
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.Tag", b =>
                {
                    b.Navigation("DishesRel");
                });

            modelBuilder.Entity("WebApplication1.Models.Entities.User", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
