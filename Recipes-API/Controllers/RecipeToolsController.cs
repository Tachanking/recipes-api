using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public RecipeToolsController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/Recipes/5/Tools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeToolDto>>> GetRecipeTools(long recipeId)
        {
            return await _context.RecipeTools.Where(rt => rt.Recipe.Id == recipeId)
                                                .Include(rt => rt.Recipe)
                                                .Include(rt => rt.Tool)
                                                .Select(rt => RecipeToolToDto(rt))
                                                .ToListAsync();
        }

        // GET: api/Recipes/5/Tools/4
        [HttpGet("{toolId}")]
        public async Task<ActionResult<RecipeToolDto>> GetRecipeTool(long recipeId, long toolId)
        {
            var recipeTool = await _context.RecipeTools.Where(rt => rt.Recipe.Id == recipeId && rt.Tool.Id == toolId)
                                                        .Include(rt => rt.Recipe)
                                                        .Include(rt => rt.Tool)
                                                        .FirstOrDefaultAsync();

            if (recipeTool is null)
            {
                return NotFound();
            }

            return RecipeToolToDto(recipeTool);
        }

        // PUT: api/Recipes/5/Tools/4
        [HttpPut("{toolId}")]
        public async Task<IActionResult> PutRecipeTool(long recipeId, long toolId, RecipeToolDto recipeToolDto)
        {
            // todo : automapper integration 
            //              flatten DTOs
            //              voir tuto pour la suite
            //        Dto validations

            _context.Entry(recipeToolDto).State = EntityState.Modified;

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
        public async Task<ActionResult<RecipeToolDto>> PostRecipeTool(long recipeId, long toolId, RecipeToolDto recipeToolDto)
        {
            var recipeTool = new RecipeTool
            {
                RecipeId = recipeId,
                Recipe = new Recipe
                {
                    Id = recipeId,
                    Name = recipeToolDto.RecipeName
                },
                Tool = new Tool
                {
                    Id = toolId,
                    Name = recipeToolDto.ToolName
                },
                Quantity = recipeToolDto.Quantity
            };

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

            return RecipeToolToDto(recipeTool);
        }

        private bool RecipeToolExists(long recipeId, long toolId)
        {
            return _context.RecipeTools.Any(rt => rt.RecipeId == recipeId && rt.ToolId == toolId);
        }

        private static RecipeToolDto RecipeToolToDto(RecipeTool recipeTool)
        {
            return new RecipeToolDto
            {
                RecipeName = recipeTool.Recipe.Name,
                ToolName = recipeTool.Tool.Name,
                Quantity = recipeTool.Quantity
            };
        }
    }
}
