using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes_API.Utility;

namespace Recipes_API.Data
{
    public class MeasurementConfiguration : IEntityTypeConfiguration<Measurement>
    {
        public void Configure(EntityTypeBuilder<Measurement> builder)
        {
            builder.ToTable("measurement");

            builder.Property(e => e.Id).HasColumnName("measurement_id").ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("measurement_name")
                .HasMaxLength(Constants.NameMaxLength);

            builder.Property(e => e.Symbol)
                .IsRequired()
                .HasColumnName("measurement_symbol")
                .HasMaxLength(Constants.SymbolMaxLength);
        }
    }
}
