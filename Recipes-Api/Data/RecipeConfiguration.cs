using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes_API.Utility;

namespace Recipes_API.Data
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("recipe");

            builder.Property(e => e.Id).HasColumnName("recipe_id").ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("recipe_name")
                .HasMaxLength(Constants.NameMaximumLength);
        }
    }
}
