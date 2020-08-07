using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes_Api.Models;
using Recipes_Api.Utility;

namespace Recipes_Api.Data
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
                .HasMaxLength(Constants.NameMaximumLength);

            builder.Property(e => e.Symbol)
                .IsRequired()
                .HasColumnName("measurement_symbol")
                .HasMaxLength(Constants.SymbolMaximumLength);
        }
    }
}
