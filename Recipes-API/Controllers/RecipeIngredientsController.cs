using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes_API.Dto;

namespace Recipes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeIngredientsController : ControllerBase
    {
        private readonly RecipesContext _context;

        public RecipeIngredientsController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/RecipeIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dto.RecipeIngredientDto>>> GetRecipeIngredients(long recipeId)
        {
            return await _context.RecipeIngredients.Where(ri => ri.RecipeId == recipeId)
                                                    .Include(ri => ri.Recipe)
                                                    .Include(ri => ri.Ingredient)
                                                    .Select(ri => RecipeIngredientToDto(ri))
                                                    .ToListAsync();
        }

        // GET: api/Recipes/5/Ingredients/4 // todo
        [HttpGet("{id}")]
        public async Task<ActionResult<Dto.RecipeIngredientDto>> GetRecipeIngredient(long recipeId, long ingredientId)  
        {
            var recipeIngredient = await _context.RecipeIngredients.Where(ri => recipeId == ri.Recipe.Id && ingredientId == ri.IngredientId)
                                                    .Include(ri => ri.Recipe)
                                                    .Include(ri => ri.Ingredient)
                                                    .FirstOrDefaultAsync();

            if (recipeIngredient is null)
            {
                return NotFound();
            }

            return RecipeIngredientToDto(recipeIngredient);
        }

        // PUT: api/Recipe/5/Ingredients/4 // todo
        [HttpPut("{id}")]
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

        // POST: api/RecipeIngredients
        [HttpPost]
        public async Task<ActionResult<Dto.RecipeIngredientDto>> PostRecipeIngredient(Dto.RecipeIngredientDto recipeIngredientDto)
        {
            var recipeIngredient = new RecipeIngredient
            {
                RecipeId = recipeIngredientDto.Recipe.Id,
                Recipe = new Recipe
                {
                    Id = recipeIngredientDto.Recipe.Id,
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

        // DELETE: api/RecipeIngredients/5
        [HttpDelete("{id}")]
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