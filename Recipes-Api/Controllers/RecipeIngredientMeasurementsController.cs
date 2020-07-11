using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public RecipeIngredientMeasurementsController(RecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        // GET: api/Recipes/5/Ingredients/5/Measurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeIngredientMeasurementDto>>> GetRecipeIngredientMeasurements(long recipeId, long ingredientId)
        {
            return await _context.RecipeIngredientMeasurement.Where(r => r.RecipeId == recipeId && r.IngredientId == ingredientId)
                                                                .Select(r => _mapper.Map<RecipeIngredientMeasurementDto>(r))
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

            return _mapper.Map<RecipeIngredientMeasurementDto>(recipeIngredientMeasurement);
        }

        // PUT: api/Recipes/5/Ingredients/5/Measurements
        [HttpPut("{measurementId}")]
        public async Task<IActionResult> PutRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId, RecipeIngredientMeasurementDto recipeIngredientMeasuermentDto)
        {
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
        public async Task<ActionResult<RecipeIngredientMeasurementDto>> PostRecipeIngredientMeasurement(RecipeIngredientMeasurementDto recipeIngredientMeasuermentDto)
        {
            var recipeIngredientMeasurement = _mapper.Map<RecipeIngredientMeasurement>(recipeIngredientMeasuermentDto);

            _context.RecipeIngredientMeasurement.Add(recipeIngredientMeasurement);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeIngredientMeasurement", new { 
                                                                            recipeId = recipeIngredientMeasuermentDto.RecipeId,
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

            return _mapper.Map<RecipeIngredientMeasurementDto>(recipeIngredientMeasurement);
        }

        private bool RecipeIngredientMeasurementExists(long recipeId, long ingredientId, long measurementId)
        {
            return _context.RecipeIngredientMeasurement.Any(e => e.RecipeId == recipeId && e.IngredientId == ingredientId && e.MeasurementId == measurementId);
        }
    }
}
