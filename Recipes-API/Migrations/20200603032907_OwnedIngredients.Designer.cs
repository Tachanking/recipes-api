﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Recipes_API;

namespace Recipes_API.Migrations
{
    [DbContext(typeof(RecipesContext))]
    [Migration("20200603032907_OwnedIngredients")]
    partial class OwnedIngredients
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:adminpack", ",,")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Recipes_API.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ingredient_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasColumnName("ingredient_name")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.Property<int>("MeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("measurement_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.HasKey("IngredientId");

                    b.HasIndex("MeasurementId");

                    b.ToTable("ingredient");
                });

            modelBuilder.Entity("Recipes_API.Measurement", b =>
                {
                    b.Property<int>("MeasurementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("measurement_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("MeasurementName")
                        .IsRequired()
                        .HasColumnName("measurement_name")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.Property<string>("MeasurementSymbol")
                        .IsRequired()
                        .HasColumnName("measurement_symbol")
                        .HasColumnType("character varying(8)")
                        .HasMaxLength(8);

                    b.HasKey("MeasurementId");

                    b.ToTable("measurement");
                });

            modelBuilder.Entity("Recipes_API.OwnedIngredient", b =>
                {
                    b.Property<int>("OwnedIngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OwnedIngredientId");

                    b.ToTable("OwnedIngredient");
                });

            modelBuilder.Entity("Recipes_API.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("recipe_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasColumnName("recipe_name")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.HasKey("RecipeId");

                    b.ToTable("recipe");
                });

            modelBuilder.Entity("Recipes_API.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("recipe_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ingredient_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Quantity")
                        .HasColumnName("quantity")
                        .HasColumnType("integer");

                    b.HasKey("RecipeId", "IngredientId")
                        .HasName("recipe_ingredient_pkey");

                    b.HasIndex("IngredientId");

                    b.ToTable("recipe_ingredient");
                });

            modelBuilder.Entity("Recipes_API.RecipeTool", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("recipe_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ToolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tool_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Quantity")
                        .HasColumnName("quantity")
                        .HasColumnType("integer");

                    b.HasKey("RecipeId", "ToolId")
                        .HasName("recipe_tool_pkey");

                    b.HasIndex("ToolId");

                    b.ToTable("recipe_tool");
                });

            modelBuilder.Entity("Recipes_API.Tool", b =>
                {
                    b.Property<int>("ToolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("tool_id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ToolName")
                        .IsRequired()
                        .HasColumnName("tool_name")
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.HasKey("ToolId");

                    b.ToTable("tool");
                });

            modelBuilder.Entity("Recipes_API.Ingredient", b =>
                {
                    b.HasOne("Recipes_API.Measurement", "Measurement")
                        .WithMany("Ingredient")
                        .HasForeignKey("MeasurementId")
                        .HasConstraintName("ingredient_measurement_id_fkey")
                        .IsRequired();
                });

            modelBuilder.Entity("Recipes_API.RecipeIngredient", b =>
                {
                    b.HasOne("Recipes_API.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredient")
                        .HasForeignKey("IngredientId")
                        .HasConstraintName("recipe_ingredient_ingredient_id_fkey")
                        .IsRequired();

                    b.HasOne("Recipes_API.Recipe", "Recipe")
                        .WithMany("RecipeIngredient")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("recipe_ingredient_recipe_id_fkey")
                        .IsRequired();
                });

            modelBuilder.Entity("Recipes_API.RecipeTool", b =>
                {
                    b.HasOne("Recipes_API.Recipe", "Recipe")
                        .WithMany("RecipeTool")
                        .HasForeignKey("RecipeId")
                        .HasConstraintName("recipe_tool_recipe_id_fkey")
                        .IsRequired();

                    b.HasOne("Recipes_API.Tool", "Tool")
                        .WithMany("RecipeTool")
                        .HasForeignKey("ToolId")
                        .HasConstraintName("recipe_tool_tool_id_fkey")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
