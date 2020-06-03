using Microsoft.EntityFrameworkCore;

namespace Recipes_API
{
    public partial class RecipesContext : DbContext
    {
        public RecipesContext()
        {
        }

        public RecipesContext(DbContextOptions<RecipesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<Measurements> Measurements { get; set; }
        public virtual DbSet<RecipeIngredients> RecipeIngredients { get; set; }
        public virtual DbSet<RecipeTools> RecipeTools { get; set; }
        public virtual DbSet<Recipes> Recipes { get; set; }
        public virtual DbSet<Tools> Tools { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack");

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.HasKey(e => e.IngredientId)
                    .HasName("ingredients_pkey");

                entity.ToTable("ingredients");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.IngredientName)
                    .IsRequired()
                    .HasColumnName("ingredient_name")
                    .HasMaxLength(64);

                entity.Property(e => e.MeasurementId)
                    .HasColumnName("measurement_id")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Measurement)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.MeasurementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ingredients_measurement_id_fkey");
            });

            modelBuilder.Entity<Measurements>(entity =>
            {
                entity.HasKey(e => e.MeasurementId)
                    .HasName("measurements_pkey");

                entity.ToTable("measurements");

                entity.Property(e => e.MeasurementId).HasColumnName("measurement_id");

                entity.Property(e => e.MeasurementName)
                    .IsRequired()
                    .HasColumnName("measurement_name")
                    .HasMaxLength(64);

                entity.Property(e => e.MeasurementSymbol)
                    .IsRequired()
                    .HasColumnName("measurement_symbol")
                    .HasMaxLength(8);
            });

            modelBuilder.Entity<RecipeIngredients>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.IngredientId })
                    .HasName("recipe_ingredients_pkey");

                entity.ToTable("recipe_ingredients");

                entity.Property(e => e.RecipeId)
                    .HasColumnName("recipe_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IngredientId)
                    .HasColumnName("ingredient_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_ingredients_ingredient_id_fkey");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeIngredients)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_ingredients_recipe_id_fkey");
            });

            modelBuilder.Entity<RecipeTools>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.ToolId })
                    .HasName("recipe_tools_pkey");

                entity.ToTable("recipe_tools");

                entity.Property(e => e.RecipeId)
                    .HasColumnName("recipe_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ToolId)
                    .HasColumnName("tool_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeTools)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_tools_recipe_id_fkey");

                entity.HasOne(d => d.Tool)
                    .WithMany(p => p.RecipeTools)
                    .HasForeignKey(d => d.ToolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("recipe_tools_tool_id_fkey");
            });

            modelBuilder.Entity<Recipes>(entity =>
            {
                entity.HasKey(e => e.RecipeId)
                    .HasName("recipes_pkey");

                entity.ToTable("recipes");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.RecipeName)
                    .IsRequired()
                    .HasColumnName("recipe_name")
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<Tools>(entity =>
            {
                entity.HasKey(e => e.ToolId)
                    .HasName("tools_pkey");

                entity.ToTable("tools");

                entity.Property(e => e.ToolId).HasColumnName("tool_id");

                entity.Property(e => e.ToolName)
                    .IsRequired()
                    .HasColumnName("tool_name")
                    .HasMaxLength(64);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
