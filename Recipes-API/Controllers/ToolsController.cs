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
    public class ToolsController : ControllerBase
    {
        private readonly RecipesContext _context;

        public ToolsController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/Tools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolDto>>> GetTools()
        {
            return await _context.Tools.Select(t => ToolToDto(t)).ToListAsync();
        }

        // GET: api/Tools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToolDto>> GetTool(long id)
        {
            var tool = await _context.Tools.FindAsync(id);

            if (tool is null)
            {
                return NotFound();
            }

            return ToolToDto(tool);
        }

        // PUT: api/Tools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTool(long id, ToolDto toolDto)
        {
            if (id != toolDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(toolDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolExists(id))
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

        // POST: api/Tools
        [HttpPost]
        public async Task<ActionResult<Tool>> PostTool(ToolDto toolDto)
        {
            var tool = new Tool 
            {
                Id = toolDto.Id,
                Name = toolDto.Name
            };

            _context.Tools.Add(tool);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostTool), toolDto);
        }

        // DELETE: api/Tools/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tool>> DeleteTool(long id)
        {
            var tool = await _context.Tools.FindAsync(id);
            if (tool is null)
            {
                return NotFound();
            }

            _context.Tools.Remove(tool);
            await _context.SaveChangesAsync();

            return tool;
        }

        private bool ToolExists(long id)
        {
            return _context.Tools.Any(t => t.Id == id);
        }

        private static ToolDto ToolToDto(Tool tool)
        {
            return new ToolDto
            {
                Id = tool.Id,
                Name = tool.Name,
            };
        }
    }
}
