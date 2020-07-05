using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes_API.Dto;

namespace Recipes_API.Controllers
{
    [Route("api/Recipes/{recipeId}/Ingredients")]
    [ApiController]
    public class RecipeIngredientsController : ControllerBase
    {
        private readonly RecipesContext _context;

        public RecipeIngredientsController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/Recipes/6/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeIngredientDto>>> GetRecipeIngredients(long recipeId)
        {
            return await _context.RecipeIngredients.Where(ri => ri.RecipeId == recipeId)
                                                    .Include(ri => ri.Recipe)
                                                    .Include(ri => ri.Ingredient)
                                                        .ThenInclude(i => i.Measurement)
                                                    .Select(ri => RecipeIngredientToDto(ri))
                                                    .ToListAsync();
        }

        // GET: api/Recipes/5/Ingredients/4
        [HttpGet("{ingredientId}")]
        public async Task<ActionResult<RecipeIngredientDto>> GetRecipeIngredient(long recipeId, long ingredientId)  
        {
            var recipeIngredient = await _context.RecipeIngredients.Where(ri => recipeId == ri.Recipe.Id && ingredientId == ri.IngredientId)
                                                    .Include(ri => ri.Recipe) // todo : recipe model without collections?
                                                    .Include(ri => ri.Ingredient)
                                                        .ThenInclude(i => i.Measurement)
                                                    .FirstOrDefaultAsync();

            if (recipeIngredient is null)
            {
                return NotFound();
            }

            return RecipeIngredientToDto(recipeIngredient);
        }

        // PUT: api/Recipes/5/Ingredients/4
        [HttpPut("{ingredientId}")]
        public async Task<IActionResult> PutRecipeIngredient(long recipeId, long ingredientId, [FromBody] RecipeIngredientDto recipeIngredientDto)
        {
            if (recipeId != recipeIngredientDto.Recipe.Id || ingredientId != recipeIngredientDto.Ingredient.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipeIngredientDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeIngredientExists(recipeId, ingredientId))
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

        // POST: api/Recipes/5/Ingredients
        [HttpPost]
        public async Task<ActionResult<RecipeIngredientDto>> PostRecipeIngredient(long recipeId, RecipeIngredientDto recipeIngredientDto)
        {
            if (recipeId != recipeIngredientDto.Recipe.Id)
            {
                return BadRequest();
            }

            var recipeIngredient = new RecipeIngredient
            {
                RecipeId = recipeId,
                Recipe = new Recipe
                {
                    Id = recipeId,
                    Name = recipeIngredientDto.Recipe.Name
                },
                IngredientId = recipeIngredientDto.Ingredient.Id,
                Ingredient = new Ingredient
                {
                    Id = recipeIngredientDto.Ingredient.Id,
                    Name = recipeIngredientDto.Ingredient.Name,
                    Measurement = new Measurement
                    {
                        Id = recipeIngredientDto.Ingredient.Measurement.Id,
                        Name = recipeIngredientDto.Ingredient.Measurement.Name,
                        Symbol = recipeIngredientDto.Ingredient.Measurement.Symbol
                    }
                }
            };

            _context.RecipeIngredients.Add(recipeIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeIngredient", recipeIngredientDto);
        }

        // DELETE: api/Recipes/5/Ingredients/5
        [HttpDelete("{ingredientId}")]
        public async Task<ActionResult<RecipeIngredientDto>> DeleteRecipeIngredient(long recipeId, long ingredientId)
        {
            var recipeIngredient = await _context.RecipeIngredients.FindAsync(recipeId, ingredientId);
            if (recipeIngredient is null)
            {
                return NotFound();
            }

            _context.RecipeIngredients.Remove(recipeIngredient);
            await _context.SaveChangesAsync();

            return RecipeIngredientToDto(recipeIngredient);
        }

        private bool RecipeIngredientExists(long recipeId, long ingredientId)
        {
            return _context.RecipeIngredients.Any(e => e.RecipeId == recipeId && e.IngredientId == ingredientId);
        }

        private static RecipeIngredientDto RecipeIngredientToDto(RecipeIngredient recipeIngredient)
        {
            return new RecipeIngredientDto
            {
                Recipe = new RecipeDto
                {
                    Id = recipeIngredient.Recipe.Id,
                    Name = recipeIngredient.Recipe.Name
                },
                Ingredient = new IngredientDto
                {
                    Id = recipeIngredient.Ingredient.Id,
                    Name = recipeIngredient.Ingredient.Name,
                    Measurement = new MeasurementDto
                    {
                        Id = recipeIngredient.Ingredient.Measurement.Id,
                        Name = recipeIngredient.Ingredient.Measurement.Name,
                        Symbol = recipeIngredient.Ingredient.Measurement.Symbol,
                    }
                }
            };            
        }
    }
}