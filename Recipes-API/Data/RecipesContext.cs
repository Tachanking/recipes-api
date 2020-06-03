﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Recipes_API
{
    public partial class RecipesContext : DbContext
    {
        public RecipesContext()
        {
        }

        public RecipesContext(DbContextOptions<RecipesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual DbSet<RecipeTool> RecipeTools { get; set; }
        public virtual DbSet<Tool> Tools { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

// TODO #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack");

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("ingredient");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.IngredientName)
                    .IsRequired()
                    .HasColumnName("ingredient_name")
                    .HasMaxLength(64);

                entity.Property(e => e.MeasurementId)
                    .HasColumnName("measurement_id")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Measurement)
                    .WithMany(p => p.Ingredient)
                    .HasForeignKey(d => d.MeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredient_measurement_id_fkey");
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.ToTable("measurement");

                entity.Property(e => e.MeasurementId).HasColumnName("measurement_id");

                entity.Property(e => e.MeasurementName)
                    .IsRequired()
                    .HasColumnName("measurement_name")
                    .HasMaxLength(64);

                entity.Property(e => e.MeasurementSymbol)
                    .IsRequired()
                    .HasColumnName("measurement_symbol")
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("recipe");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.RecipeName)
                    .IsRequired()
                    .HasColumnName("recipe_name")
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.IngredientId })
                    .HasName("recipe_ingredient_pkey");

                entity.ToTable("recipe_ingredient");

                entity.Property(e => e.RecipeId)
                    .HasColumnName("recipe_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IngredientId)
                    .HasColumnName("ingredient_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeIngredient)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_ingredient_ingredient_id_fkey");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngredient)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_ingredient_recipe_id_fkey");
            });

            modelBuilder.Entity<RecipeTool>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.ToolId })
                    .HasName("recipe_tool_pkey");

                entity.ToTable("recipe_tool");

                entity.Property(e => e.RecipeId)
                    .HasColumnName("recipe_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ToolId)
                    .HasColumnName("tool_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeTool)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_tool_recipe_id_fkey");

                entity.HasOne(d => d.Tool)
                    .WithMany(p => p.RecipeTool)
                    .HasForeignKey(d => d.ToolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_tool_tool_id_fkey");
            });

            modelBuilder.Entity<Tool>(entity =>
            {
                entity.ToTable("tool");

                entity.Property(e => e.ToolId).HasColumnName("tool_id");

                entity.Property(e => e.ToolName)
                    .IsRequired()
                    .HasColumnName("tool_name")
                    .HasMaxLength(64);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
