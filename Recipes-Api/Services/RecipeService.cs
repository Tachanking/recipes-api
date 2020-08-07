using Recipes_Api;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recipes_Api.Models;
using System;

namespace Recipes_Api.Services
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
            var recipe = await _context.Recipes.Include(r => r.RecipeTool)
                                                .ThenInclude(rt => rt.Tool)
                                            .Include(r => r.RecipeIngredientMeasurement)
                                                .ThenInclude(rim => rim.Ingredient)
                                            .Include(r => r.RecipeIngredientMeasurement)
                                                .ThenInclude(rim => rim.Measurement)
                                            .Where(r => r.Id == id)
                                            .FirstOrDefaultAsync();

            return recipe;
        }

        public async Task<bool> PutRecipe(long id, Recipe recipe)
        {
            _context.Entry(recipe).State = EntityState.Modified;

            foreach(var recipeIngredientMeasurement in recipe.RecipeIngredientMeasurement)
            {
                _context.Entry(recipeIngredientMeasurement.Ingredient).State = EntityState.Modified;
                _context.Entry(recipeIngredientMeasurement.Measurement).State = EntityState.Modified;
            }

            foreach (var recipeTool in recipe.RecipeTool)
            {
                _context.Entry(recipeTool.Tool).State = EntityState.Modified;
            }

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
            return recipe;
        }

        public async Task<bool> DeleteRecipe(long id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe is null)
                return false;
            // todo : error handling

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool RecipeExists(long id)
        {
            return _context.Recipes.Any(r => r.Id == id);
        }
    }
}
