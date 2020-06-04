using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes_API.Data
{
    public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder.ToTable("measurement");

            builder.Property(e => e.Id).HasColumnName("measurement_id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("measurement_name")
                .HasMaxLength(64);

            builder.Property(e => e.Symbol)
                .IsRequired()
                .HasColumnName("measurement_symbol")
                .HasMaxLength(8);
        }
    }
}
