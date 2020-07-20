using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes_Api.Dto;

namespace Recipes_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipesContext _context;
        private readonly IMapper _mapper;

        public RecipesController(RecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
        {
            return await _context.Recipes.Include(r => r.RecipeTool)
                                                .ThenInclude(rt => rt.Tool)
                                            .Include(r => r.RecipeIngredientMeasurement)
                                                .ThenInclude(rim => rim.Ingredient)
                                            .Include(r => r.RecipeIngredientMeasurement)
                                                .ThenInclude(rim => rim.Measurement)
                                            .Select(r => _mapper.Map<RecipeDto>(r))
                                            .ToListAsync();
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDto>> GetRecipe(long id)
        {
            var recipe = await _context.Recipes.Include(r => r.RecipeTool) // todo : DRY
                                                .ThenInclude(rt => rt.Tool)
                                            .Include(r => r.RecipeIngredientMeasurement)
                                                .ThenInclude(rim => rim.Ingredient)
                                            .Include(r => r.RecipeIngredientMeasurement)
                                                .ThenInclude(rim => rim.Measurement)
                                            .Where(r => r.Id == id)
                                            .FirstOrDefaultAsync();
            if (recipe is null)
                return NotFound();

            return _mapper.Map<RecipeDto>(recipe);
        }

        // PUT: api/Recipes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(long id, RecipeDto recipeDto)
        {
            if (id != recipeDto.Id)
                return BadRequest();

            var recipe = _mapper.Map<Recipe>(recipeDto);
            _context.Entry(recipe).State = EntityState.Modified;

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
            var recipe = _mapper.Map<Recipe>(recipeDto);

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostRecipe), recipeDto);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeDto>> DeleteRecipe(long id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe is null)
                return NotFound();

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeDto>(recipe);
        }

        // GET: api/Recipes/5/Tools
        [HttpGet("{recipeId}/Tools")]
        public async Task<ActionResult<IEnumerable<ToolDto>>> GetRecipeTools(long recipeId)
        {
            return await _context.Tools.Include(t => t.RecipeTool)
                                        .Where(r => r.RecipeTool.Any(rt => rt.RecipeId == recipeId))
                                        .Select(rt => _mapper.Map<ToolDto>(rt))
                                        .ToListAsync();
        }

        // GET: api/Recipes/5/Tools/4
        [HttpGet("{recipeId}/Tools/{toolId}")]
        public async Task<ActionResult<ToolDto>> GetRecipeTool(long recipeId, long toolId)
        {
            var tool = await _context.Tools.Include(t => t.RecipeTool)
                                                .Where(r => r.RecipeTool.Any(rt => rt.RecipeId == recipeId &&
                                                                                rt.ToolId == toolId)
                                                    ).FirstOrDefaultAsync();

            if (tool is null)
            {
                return NotFound();
            }

            return _mapper.Map<ToolDto>(tool);
        }

        // GET: api/Recipes/5/Ingredients/5/Measurements
        [HttpGet("{recipeId}/Ingredients/{ingredientId}/Measurements")]
        public async Task<ActionResult<IEnumerable<MeasurementDto>>> GetRecipeIngredientMeasurements(long recipeId, long ingredientId)
        {
            return await _context.Measurements.Include(m => m.RecipeIngredientMeasurement)
                                                .Where(r => r.RecipeIngredientMeasurement.Any(rim => rim.RecipeId == recipeId && 
                                                                                                rim.IngredientId == ingredientId))
                                                .Select(r => _mapper.Map<MeasurementDto>(r))
                                                .ToListAsync();
        }

        // GET: api/Recipes/5/Ingredients/5/Measurements/5
        [HttpGet("{recipeId}/Ingredients/{ingredientId}/Measurements/{measurementId}")]
        public async Task<ActionResult<MeasurementDto>> GetRecipeIngredientMeasurement(long recipeId, long ingredientId, long measurementId)
        {
            var measurement = await _context.Measurements.Include(m => m.RecipeIngredientMeasurement)
                                                            .Where(r => r.RecipeIngredientMeasurement.Any(rim => rim.RecipeId == recipeId &&
                                                                                                                    rim.IngredientId == ingredientId &&
                                                                                                                    rim.MeasurementId == measurementId)
                                                            ).FirstOrDefaultAsync();

            if (measurement == null)
            {
                return NotFound();
            }

            return _mapper.Map<MeasurementDto>(measurement);
        }

        private bool RecipeExists(long id)
        {
            return _context.Recipes.Any(r => r.Id == id);
        }
    }
}
