using Microsoft.EntityFrameworkCore;
using Recipes_API.Data;
using Recipes_API.Models;

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

        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Measurement> Measurements { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }
        public virtual DbSet<RecipeIngredientMeasurement> RecipeIngredientMeasurements { get; set; }
        public virtual DbSet<RecipeTool> RecipeTools { get; set; }
        public virtual DbSet<Tool> Tools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack");

            modelBuilder.ApplyConfiguration(new IngredientConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeIngredientMeasurementConfiguration());
            modelBuilder.ApplyConfiguration(new MeasurementConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeToolConfiguration());
            modelBuilder.ApplyConfiguration(new ToolConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Recipes_API.Models.RecipeIngredientMeasurement> RecipeIngredientMeasurement { get; set; }
    }
}
