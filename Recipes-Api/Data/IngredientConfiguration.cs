using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes_API.Utility;

namespace Recipes_API.Data
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("ingredient");

            builder.Property(e => e.Id).HasColumnName("ingredient_id").ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("ingredient_name")
                .HasMaxLength(Constants.NameMaxLength);
        }
    }
}
