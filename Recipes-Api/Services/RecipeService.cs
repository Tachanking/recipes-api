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


        // todo from here
        public async Task PutRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId, Measurement measurement)
        {
            _context.Entry(measurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientMeasurementExists(recipeId, ingredientId, measurementId))
                {
                    ////return NotFound();

                    // todo : error handling
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Measurement> PostRecipeIngredientMeasurement(long recipeId, long ingredientId, Measurement measurement)
        {
            // todo : Get recipe
            // if not exist, throw
            //          get ingredient
            // if not exist, throw,
            // Recipe.Ingredient[id].Measurements.add(measurement);
            await _context.SaveChangesAsync();
            return measurement;
        }

        public async Task<Measurement> DeleteRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId)
        {
            throw new NotImplementedException();

            var recipeIngredientMeasurement = await _context.RecipeIngredientMeasurement.Where(r => r.RecipeId == recipeId &&
                                                                                              r.IngredientId == ingredientId &&
                                                                                              r.MeasurementId == measurementId
                                                                                    ).FirstOrDefaultAsync();
            if (recipeIngredientMeasurement == null)
            {
                ////return NotFound();
                // todo : error handling
            }

            _context.RecipeIngredientMeasurement.Remove(recipeIngredientMeasurement);
            await _context.SaveChangesAsync();

            return null;
        }

        public async Task PutRecipeTool(long recipeId, long toolId, Tool tool)
        {
            _context.Entry(tool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeToolExists(recipeId, toolId))
                {
                    //return NotFound();
                    // todo : error handling
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<Tool> PostRecipeTool(Tool tool)
        {
            throw new NotImplementedException();

            //_context.RecipeTools.Add(tool);
            //await _context.SaveChangesAsync();
            //return tool;
        }

        public async Task<Tool> DeleteRecipeTool(long recipeId, long toolId)
        {
            throw new NotImplementedException("Fix incoming");

            var recipeTool = await _context.RecipeTools.FindAsync(recipeId, toolId);
            if (recipeTool is null)
            {
                //return NotFound();
                // todo : error handling
            }

            _context.RecipeTools.Remove(recipeTool);
            await _context.SaveChangesAsync();

            return null;
        }

        private bool RecipeIngredientMeasurementExists(long recipeId, long ingredientId, long measurementId)
        {
            return _context.RecipeIngredientMeasurement.Any(e => e.RecipeId == recipeId && e.IngredientId == ingredientId && e.MeasurementId == measurementId);
        }

        private bool RecipeExists(long id)
        {
            return _context.Recipes.Any(r => r.Id == id);
        }

        private bool RecipeToolExists(long recipeId, long toolId)
        {
            return _context.RecipeTools.Any(rt => rt.RecipeId == recipeId && rt.ToolId == toolId);
        }
    }
}
