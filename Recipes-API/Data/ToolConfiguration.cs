using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Recipes_API.Data
{
    public class ToolConfiguration : IEntityTypeConfiguration<Tool>
    {
        public void Configure(EntityTypeBuilder<Tool> builder)
        {
            builder.ToTable("tool");

            builder.Property(e => e.ToolId).HasColumnName("tool_id");

            builder.Property(e => e.ToolName)
                .IsRequired()
                .HasColumnName("tool_name")
                .HasMaxLength(64);
        }
    }
}
