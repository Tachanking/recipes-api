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
    }
}
