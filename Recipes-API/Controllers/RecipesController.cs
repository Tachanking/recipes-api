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
    public class RecipesController : ControllerBase
    {
        private readonly RecipesContext _context;

        public RecipesController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
        {
            return await _context.Recipes.Include(r => r.RecipeIngredient)
                                            .ThenInclude(re => re.Ingredient)
                                            .ThenInclude(i => i.Measurement)
                                            .Include(r => r.RecipeTool)
                                            .ThenInclude(r => r.Tool)
                                            .Select(r => RecipeToDto(r))
                                            .ToListAsync();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.Where(r => r.Id == id)
                                                .Include(r => r.RecipeIngredient)
                                                .ThenInclude(re => re.Ingredient)
                                                .ThenInclude(i => i.Measurement)
                                                .Include(r => r.RecipeTool)
                                                .ThenInclude(r => r.Tool)
                                                .FirstOrDefaultAsync();

            if (recipe is null)
            {
                return NotFound();
            }

            return RecipeToDto(recipe);
        }

        // PUT: api/Recipes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, RecipeDto recipeDto)
        {
            if (id != recipeDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipeDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipes
        [HttpPost]
        public async Task<ActionResult<RecipeDto>> PostRecipe(RecipeDto recipeDto)
        {
            var recipe = new Recipe
            {
                Id = recipeDto.Id,
                Name = recipeDto.Name
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostRecipe), recipeDto);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeDto>> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe is null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return RecipeToDto(recipe);
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(r => r.Id == id);
        }

        private static RecipeDto RecipeToDto(Recipe recipe)
        {
            return new RecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name
            };
        }
    }
}
