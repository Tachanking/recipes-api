using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes_API.Data
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("recipe");

            builder.Property(e => e.Id).HasColumnName("recipe_id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("recipe_name")
                .HasMaxLength(64);
        }
    }
}
