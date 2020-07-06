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
    public class IngredientsController : ControllerBase
    {
        private readonly RecipesContext _context;

        public IngredientsController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetIngredients()
        { 
            return await _context.Ingredients.Select(i => IngredientToDto(i))
                                                .ToListAsync();
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDto>> GetIngredient(int id)
        {
            var ingredient = await _context.Ingredients.Where(i => i.Id == id)
                                                        .FirstOrDefaultAsync();

            if (ingredient is null)
            {
                return NotFound();
            }

            return IngredientToDto(ingredient);
        }

        // PUT: api/Ingredients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(long id, IngredientDto ingredientDto)
        {
            if (id != ingredientDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingredientDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredients
        [HttpPost]
        public async Task<ActionResult<IngredientDto>> PostIngredient(IngredientDto ingredientDto)
        {
            var ingredient = new Ingredient
            {
                Name = ingredientDto.Name
            };

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostIngredient), ingredientDto);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IngredientDto>> DeleteIngredient(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient is null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return IngredientToDto(ingredient);
        }

        private bool IngredientExists(long id)
        {
            return _context.Ingredients.Any(i => i.Id == id);
        }

        private static IngredientDto IngredientToDto(Ingredient ingredient)
        {
            return new IngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name
            };
        }
    }
}
