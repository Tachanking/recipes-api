using Recipes_Api;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Recipes_API.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeContext _context;

        public RecipeService(RecipeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetRecipes()
        {
            // todo : service classes
            return await _context.Recipes.Include(r => r.RecipeTool)
                                                .ThenInclude(rt => rt.Tool)
                                            .Include(r => r.RecipeIngredientMeasurement)
                                                .ThenInclude(rim => rim.Ingredient)
                                            .Include(r => r.RecipeIngredientMeasurement)
                                                .ThenInclude(rim => rim.Measurement)
                                            .ToListAsync();
        }

        public async Task<Recipe> GetRecipe(long id)
        {
            var recipe = await _context.Recipes.Include(r => r.RecipeTool) // todo : DRY
                                                .ThenInclude(rt => rt.Tool)
                                            .Include(r => r.RecipeIngredientMeasurement)
                                                .ThenInclude(rim => rim.Ingredient)
                                            .Include(r => r.RecipeIngredientMeasurement)
                                                .ThenInclude(rim => rim.Measurement)
                                            .Where(r => r.Id == id)
                                            .FirstOrDefaultAsync();

            return recipe;
        }

        // todo : fk validations
        public async Task<bool> PutRecipe(long id, Recipe recipe)
        {
            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<Recipe> PostRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            // todo : fk validations

            return recipe;
        }

        public async Task<bool> DeleteRecipe(long id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe is null)
                return false;

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Tool>> GetRecipeTools(long recipeId)
        {
            return await _context.Tools.Include(t => t.RecipeTool)
                                        .Where(r => r.RecipeTool.Any(rt => rt.RecipeId == recipeId))
                                        .ToListAsync();
        }

        public async Task<Tool> GetRecipeTool(long recipeId, long toolId)
        {
            var tool = await _context.Tools.Include(t => t.RecipeTool)
                                                .Where(r => r.RecipeTool.Any(rt => rt.RecipeId == recipeId &&
                                                                                rt.ToolId == toolId)
                                                    ).FirstOrDefaultAsync();

            return tool;
        }

        public async Task<IEnumerable<Measurement>> GetRecipeIngredientMeasurements(long recipeId, long ingredientId)
        {
            return await _context.Measurements.Include(m => m.RecipeIngredientMeasurement)
                                                .Where(r => r.RecipeIngredientMeasurement.Any(rim => rim.RecipeId == recipeId &&
                                                                                                rim.IngredientId == ingredientId))
                                                .ToListAsync();
        }

        public async Task<Measurement> GetRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId)
        {
            var measurement = await _context.Measurements.Include(m => m.RecipeIngredientMeasurement)
                                                            .Where(r => r.RecipeIngredientMeasurement.Any(rim => rim.RecipeId == recipeId &&
                                                                                                                    rim.IngredientId == ingredientId &&
                                                                                                                    rim.MeasurementId == measurementId)
                                                            ).FirstOrDefaultAsync();            

            return measurement;
        }

        private bool RecipeExists(long id)
        {
            return _context.Recipes.Any(r => r.Id == id);
        }


        // todo:  validations + tests HERE
        private bool IngredientExists(long id)
        {
            return _context.Ingredients.Any(r => r.Id == id);
        }

        private bool MeasurementExists(long id)
        {
            return _context.Measurements.Any(r => r.Id == id);
        }

        private bool ToolExists(long id)
        {
            return _context.Tools.Any(r => r.Id == id);
        }
    }
}
