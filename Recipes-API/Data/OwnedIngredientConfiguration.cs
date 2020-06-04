using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes_API.Data
{
    public class OwnedIngredientConfiguration : IEntityTypeConfiguration<OwnedIngredient>
    {
        public void Configure(EntityTypeBuilder<OwnedIngredient> builder)
        {
            builder.HasKey(e => new { e.Id })
                .HasName("owned_ingredient_pkey");

            builder.ToTable("owned_ingredient");

            builder.Property(e => e.Id)
                .HasColumnName("owned_ingredient_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.IngredientId)
                .HasColumnName("ingredient_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Quantity).HasColumnName("quantity");
        }
    }
}
