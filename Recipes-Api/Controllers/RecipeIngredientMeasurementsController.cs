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
            if (recipeId != recipeIngredientMeasuermentDto.RecipeId || 
                ingredientId != recipeIngredientMeasuermentDto.IngredientId || 
                measurementId != recipeIngredientMeasuermentDto.MeasurementId)
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
            if (recipeId != recipeIngredientMeasuermentDto.RecipeId ||
                ingredientId != recipeIngredientMeasuermentDto.IngredientId)
            {
                return BadRequest();
            }

            var recipeIngredientMeasurement = new RecipeIngredientMeasurement
            {
                RecipeId = recipeIngredientMeasuermentDto.RecipeId,
                IngredientId = recipeIngredientMeasuermentDto.IngredientId,
                MeasurementId = recipeIngredientMeasuermentDto.MeasurementId,

                Quantity = recipeIngredientMeasuermentDto.Quantity
            };

            _context.RecipeIngredientMeasurement.Add(recipeIngredientMeasurement);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeIngredientMeasurement", new { 
                                                                            recipeId = recipeIngredientMeasuermentDto.IngredientId, 
                                                                            ingredientId = recipeIngredientMeasuermentDto.IngredientId,
                                                                            measurementId = recipeIngredientMeasuermentDto.MeasurementId 
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
                RecipeId = recipeIngredientMeasurement.RecipeId,
                IngredientId = recipeIngredientMeasurement.IngredientId,                   
                MeasurementId = recipeIngredientMeasurement.MeasurementId,

                Quantity = recipeIngredientMeasurement.Quantity
            };
        }
    }
}
