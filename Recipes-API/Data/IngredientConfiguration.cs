using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes_API.Data
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("ingredient");

            builder.Property(e => e.Id).HasColumnName("ingredient_id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("ingredient_name")
                .HasMaxLength(64);

            builder.Property(e => e.MeasurementId)
                .HasColumnName("measurement_id")
                .ValueGeneratedOnAdd();

            builder.HasOne(d => d.Measurement)
                .WithMany(p => p.Ingredient)
                .HasForeignKey(d => d.MeasurementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_measurement_id_fkey");
        }
    }
}
