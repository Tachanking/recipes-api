using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes_Api.Data
{
    public class RecipeToolConfiguration : IEntityTypeConfiguration<RecipeTool>
    {
        public void Configure(EntityTypeBuilder<RecipeTool> builder)
        {
            builder.HasKey(e => new { e.RecipeId, e.ToolId })
                    .HasName("recipe_tool_pkey");

            builder.ToTable("recipe_tool");

            builder.Property(e => e.RecipeId)
                .HasColumnName("recipe_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.ToolId)
                .HasColumnName("tool_id")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Quantity).HasColumnName("quantity");

            builder.HasOne(d => d.Recipe)
                .WithMany(p => p.RecipeTool)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_tool_recipe_id_fkey");

            builder.HasOne(d => d.Tool)
                .WithMany(p => p.RecipeTool)
                .HasForeignKey(d => d.ToolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_tool_tool_id_fkey");
        }
    }
}
