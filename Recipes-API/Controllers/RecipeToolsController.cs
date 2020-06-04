using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipes_API;

namespace Recipes_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeToolsController : ControllerBase
    {
        private readonly RecipesContext _context;

        public RecipeToolsController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/RecipeTools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeTool>>> GetRecipeTools()
        {
            return await _context.RecipeTools.ToListAsync();
        }

        // GET: api/RecipeTools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeTool>> GetRecipeTool(int id)
        {
            var recipeTool = await _context.RecipeTools.FindAsync(id);

            if (recipeTool == null)
            {
                return NotFound();
            }

            return recipeTool;
        }

        // PUT: api/RecipeTools/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipeTool(int id, RecipeTool recipeTool)
        {
            if (id != recipeTool.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(recipeTool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeToolExists(id))
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

        // POST: api/RecipeTools
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RecipeTool>> PostRecipeTool(RecipeTool recipeTool)
        {
            _context.RecipeTools.Add(recipeTool);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostRecipeTool), new { id = recipeTool.RecipeId }, recipeTool);
        }

        // DELETE: api/RecipeTools/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeTool>> DeleteRecipeTool(int id)
        {
            var recipeTool = await _context.RecipeTools.FindAsync(id);
            if (recipeTool == null)
            {
                return NotFound();
            }

            _context.RecipeTools.Remove(recipeTool);
            await _context.SaveChangesAsync();

            return recipeTool;
        }

        private bool RecipeToolExists(int id)
        {
            return _context.RecipeTools.Any(e => e.RecipeId == id);
        }
    }
}
