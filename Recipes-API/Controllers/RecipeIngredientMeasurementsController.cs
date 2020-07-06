using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes_API.Dto;
using Recipes_API.Models;

namespace Recipes_API.Controllers
{
    [Route("api/Recipes/{recipeId}/Ingredients/{ingredientId}/Measurements")]
    [ApiController]
    public class RecipeIngredientMeasurementsController : ControllerBase
    {
        private readonly RecipesContext _context;

        public RecipeIngredientMeasurementsController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/Recipes/5/Ingredients/5/Measurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeIngredientMeasurementDto>>> GetRecipeIngredientMeasurements(long recipeId, long ingredientId)
        {
            return await _context.RecipeIngredientMeasurement.Where(r => r.RecipeId == recipeId && r.IngredientId == ingredientId)
                                                                .Select(r => RecipeIngredientMeasurementToDto(r))
                                                                .ToListAsync();

            // todo : NotFound(); ???
        }

        // GET: api/Recipes/5/Ingredients/5/Measurements/5
        [HttpGet("{measurementId}")]
        public async Task<ActionResult<RecipeIngredientMeasurementDto>> GetRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId)
        {
            var recipeIngredientMeasurement = await _context.RecipeIngredientMeasurement.Where(r => r.RecipeId == recipeId &&
                                                                                               r.IngredientId == ingredientId &&
                                                                                               r.MeasurementId == measurementId
                                                                                            ).FirstOrDefaultAsync();

            if (recipeIngredientMeasurement == null)
            {
                return NotFound();
            }

            return RecipeIngredientMeasurementToDto(recipeIngredientMeasurement);
        }

        // PUT: api/Recipes/5/Ingredients/5/Measurements
        [HttpPut("{measurementId}")]
        public async Task<IActionResult> PutRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId, RecipeIngredientMeasurementDto recipeIngredientMeasuermentDto)
        {
            if (recipeId != recipeIngredientMeasuermentDto.Recipe.Id || 
                ingredientId != recipeIngredientMeasuermentDto.Ingredient.Id || 
                measurementId != recipeIngredientMeasuermentDto.Measurement.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipeIngredientMeasuermentDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientMeasurementExists(recipeId, ingredientId, measurementId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipes/5/Ingredients/5/Measurements
        [HttpPost]
        public async Task<ActionResult<RecipeIngredientMeasurementDto>> PostRecipeIngredientMeasurement(long recipeId, long ingredientId, RecipeIngredientMeasurementDto recipeIngredientMeasuermentDto)
        {
            if (recipeId != recipeIngredientMeasuermentDto.Recipe.Id ||
                ingredientId != recipeIngredientMeasuermentDto.Ingredient.Id)
            {
                return BadRequest();
            }

            var recipeIngredientMeasurement = new RecipeIngredientMeasurement
            {
                RecipeId = recipeIngredientMeasuermentDto.Recipe.Id,
                Recipe = new Recipe
                {
                    Id = recipeIngredientMeasuermentDto.Recipe.Id,
                    Name = recipeIngredientMeasuermentDto.Recipe.Name
                },
                IngredientId = recipeIngredientMeasuermentDto.Ingredient.Id,
                Ingredient = new Ingredient
                {
                    Id = recipeIngredientMeasuermentDto.Ingredient.Id,
                    Name = recipeIngredientMeasuermentDto.Ingredient.Name,
                },
                MeasurementId = recipeIngredientMeasuermentDto.Measurement.Id,
                Measurement = new Measurement
                {
                    Id = recipeIngredientMeasuermentDto.Measurement.Id,
                    Name = recipeIngredientMeasuermentDto.Measurement.Name,
                    Symbol = recipeIngredientMeasuermentDto.Measurement.Symbol
                },
                Quantity = recipeIngredientMeasuermentDto.Quantity
            };

            _context.RecipeIngredientMeasurement.Add(recipeIngredientMeasurement);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeIngredientMeasurement", new { 
                                                                            recipeId = recipeIngredientMeasuermentDto.Ingredient.Id, 
                                                                            ingredientId = recipeIngredientMeasuermentDto.Ingredient.Id,
                                                                            measurementId = recipeIngredientMeasuermentDto.Measurement.Id 
                                                                        }, recipeIngredientMeasuermentDto);
        }

        // DELETE: api/Recipes/5/Ingredients/5/Measurements/5
        [HttpDelete("{measurementId}")]
        public async Task<ActionResult<RecipeIngredientMeasurementDto>> DeleteRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId)
        {
            var recipeIngredientMeasurement = await _context.RecipeIngredientMeasurement.Where(r => r.RecipeId == recipeId &&
                                                                                              r.IngredientId == ingredientId &&
                                                                                              r.MeasurementId == measurementId
                                                                                    ).FirstOrDefaultAsync();
            if (recipeIngredientMeasurement == null)
            {
                return NotFound();
            }

            _context.RecipeIngredientMeasurement.Remove(recipeIngredientMeasurement);
            await _context.SaveChangesAsync();

            return RecipeIngredientMeasurementToDto(recipeIngredientMeasurement);
        }

        private bool RecipeIngredientMeasurementExists(long recipeId, long ingredientId, long measurementId)
        {
            return _context.RecipeIngredientMeasurement.Any(e => e.RecipeId == recipeId && e.IngredientId == ingredientId && e.MeasurementId == measurementId);
        }

        private static RecipeIngredientMeasurementDto RecipeIngredientMeasurementToDto(RecipeIngredientMeasurement recipeIngredientMeasurement)
        {
            return new RecipeIngredientMeasurementDto
            {
                Recipe = new RecipeDto
                {
                    Id = recipeIngredientMeasurement.RecipeId,
                    Name = recipeIngredientMeasurement.Recipe.Name
                },
                Ingredient = new IngredientDto
                {
                    Id = recipeIngredientMeasurement.IngredientId,
                    Name = recipeIngredientMeasurement.Ingredient.Name
                }, 
                Measurement = new MeasurementDto
                {
                    Id = recipeIngredientMeasurement.MeasurementId,
                    Name = recipeIngredientMeasurement.Measurement.Name,
                    Symbol = recipeIngredientMeasurement.Measurement.Symbol
                },
                Quantity = recipeIngredientMeasurement.Quantity
            };
        }
    }
}
