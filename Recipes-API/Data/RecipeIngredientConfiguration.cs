using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes_API.Data
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.ToTable("recipe_ingredient");

            builder.HasKey(e => new { e.RecipeId, e.IngredientId })
                .HasName("recipe_ingredient_pkey");

            builder.Property(e => e.RecipeId)
                .HasColumnName("recipe_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IngredientId)
                .HasColumnName("ingredient_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IsOptional)
                .HasColumnName("is_optional")
                .HasDefaultValue(false);

            builder.Property(e => e.Quantity).HasColumnName("quantity");

            builder.HasOne(d => d.Ingredient)
                .WithMany(p => p.RecipeIngredient)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_ingredient_ingredient_id_fkey");

            builder.HasOne(d => d.Recipe)
                .WithMany(p => p.RecipeIngredient)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_ingredient_recipe_id_fkey");
        }
    }
}
