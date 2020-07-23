using Recipes_Api;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes_API.Services
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
    }
}
