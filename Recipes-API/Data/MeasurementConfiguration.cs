using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes_API.Data
{
    public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder.ToTable("measurement");

            builder.Property(e => e.MeasurementId).HasColumnName("measurement_id");

            builder.Property(e => e.MeasurementName)
                .IsRequired()
                .HasColumnName("measurement_name")
                .HasMaxLength(64);

            builder.Property(e => e.MeasurementSymbol)
                .IsRequired()
                .HasColumnName("measurement_symbol")
                .HasMaxLength(8);
        }
    }
}
