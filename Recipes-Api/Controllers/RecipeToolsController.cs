using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes_API.Dto;

namespace Recipes_API.Controllers
{
    [Route("api/Recipes/{recipeId}/Tools")]
    [ApiController]
    public class RecipeToolsController : ControllerBase
    {
        private readonly RecipesContext _context;
        private readonly IMapper _mapper;

        public RecipeToolsController(RecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Recipes/5/Tools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeToolDto>>> GetRecipeTools(long recipeId)
        {
            return await _context.RecipeTools.Where(rt => rt.Recipe.Id == recipeId)
                                                .Select(rt => _mapper.Map<RecipeToolDto>(rt))
                                                .ToListAsync();
        }

        // GET: api/Recipes/5/Tools/4
        [HttpGet("{toolId}")]
        public async Task<ActionResult<RecipeToolDto>> GetRecipeTool(long recipeId, long toolId)
        {
            var recipeTool = await _context.RecipeTools.Where(rt => rt.Recipe.Id == recipeId && rt.Tool.Id == toolId).FirstOrDefaultAsync();

            if (recipeTool is null)
            {
                return NotFound();
            }

            return _mapper.Map<RecipeToolDto>(recipeTool);
        }

        // PUT: api/Recipes/5/Tools/4
        [HttpPut("{toolId}")]
        public async Task<IActionResult> PutRecipeTool(long recipeId, long toolId, RecipeToolDto recipeToolDto) // todo : IDs?
        {
            var recipeTool = _mapper.Map<RecipeTool>(recipeToolDto);
            _context.Entry(recipeTool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeToolExists(recipeId, toolId))
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

        // POST: api/Recipes/5/Tools
        [HttpPost]
        public async Task<ActionResult<RecipeToolDto>> PostRecipeTool(RecipeToolDto recipeToolDto)
        {
            var recipeTool = _mapper.Map<RecipeTool>(recipeToolDto);

            _context.RecipeTools.Add(recipeTool);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipeTool", recipeToolDto);
        }

        // DELETE: api/Recipes/5/Tools/5
        [HttpDelete("{toolId}")]
        public async Task<ActionResult<RecipeToolDto>> DeleteRecipeTool(long recipeId, long toolId)
        {
            var recipeTool = await _context.RecipeTools.FindAsync(recipeId, toolId);
            if (recipeTool is null)
            {
                return NotFound();
            }

            _context.RecipeTools.Remove(recipeTool);
            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeToolDto>(recipeTool);
        }

        private bool RecipeToolExists(long recipeId, long toolId)
        {
            return _context.RecipeTools.Any(rt => rt.RecipeId == recipeId && rt.ToolId == toolId);
        }
    }
}
