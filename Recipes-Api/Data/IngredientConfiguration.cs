using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes_Api.Utility;

namespace Recipes_Api.Data
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
                .HasMaxLength(Constants.NameMaximumLength);
        }
    }
}
