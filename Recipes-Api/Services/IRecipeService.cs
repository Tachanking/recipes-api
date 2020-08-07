using Recipes_Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes_Api.Services
{
    public interface IRecipeService
    {
        public Task<IEnumerable<Recipe>> GetRecipes();

        public Task<Recipe> GetRecipe(long id);

        public Task<bool> PutRecipe(long id, Recipe recipe);

        public Task<Recipe> PostRecipe(Recipe recipe);

        public Task<bool> DeleteRecipe(long id);

        public Task<IEnumerable<Tool>> GetRecipeTools(long recipeId);

        public Task<Tool> GetRecipeTool(long recipeId, long toolId);

        public Task<IEnumerable<Measurement>> GetRecipeIngredientMeasurements(long recipeId, long ingredientId);

        public Task<Measurement> GetRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId);

        public Task PutRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId, Measurement measurement);

        public Task<Measurement> PostRecipeIngredientMeasurement(long recipeId, long ingredientId, Measurement measurement);

        public Task<Measurement> DeleteRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId);

        public Task PutRecipeTool(long recipeId, long toolId, Tool tool);

        public Task<Tool> PostRecipeTool(Tool tool);

        public Task<Tool> DeleteRecipeTool(long recipeId, long toolId);
    }
}
