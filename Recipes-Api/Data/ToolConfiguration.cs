using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Recipes_API.Utility;

namespace Recipes_API.Data
{
    public class ToolConfiguration : IEntityTypeConfiguration<Tool>
    {
        public void Configure(EntityTypeBuilder<Tool> builder)
        {
            builder.ToTable("tool");

            builder.Property(e => e.Id).HasColumnName("tool_id").ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("tool_name")
                .HasMaxLength(Constants.NameMaximumLength);
        }
    }
}
