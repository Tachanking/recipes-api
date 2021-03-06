﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes_Api.Models;

namespace Recipes_Api.Data
{
    public class RecipeIngredientMeasurementConfiguration : IEntityTypeConfiguration<RecipeIngredientMeasurement>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredientMeasurement> builder)
        {
            builder.ToTable("recipe_ingredient_measurement");

            builder.HasKey(e => new { e.RecipeId, e.IngredientId, e.MeasurementId })
                .HasName("recipe_ingredient_measurement_pkey");

            builder.Property(e => e.Quantity).HasColumnName("quantity");

            builder.HasOne(d => d.Recipe)
                .WithMany(p => p.RecipeIngredientMeasurement)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("recipe_ingredient_measurement_recipe_id_fkey");

            builder.HasOne(d => d.Ingredient)
                .WithMany(p => p.RecipeIngredientMeasurement)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("recipe_ingredient_measurement_ingredient_id_fkey");

            builder.HasOne(d => d.Measurement)
                .WithMany(p => p.RecipeIngredientMeasurement)
                .HasForeignKey(d => d.MeasurementId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("recipe_ingredient_measurement_measurement_id_fkey");
        }
    }
}
