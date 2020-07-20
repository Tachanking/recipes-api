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
    public class ToolsController : ControllerBase
    {
        private readonly RecipesContext _context;
        private readonly IMapper _mapper;

        public ToolsController(RecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolDto>>> GetTools()
        {
            return await _context.Tools.Select(t => _mapper.Map<Tool, ToolDto>(t)).ToListAsync();
        }

        // GET: api/Tools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToolDto>> GetTool(long id)
        {
            var tool = await _context.Tools.FindAsync(id);
            if (tool is null)
                return NotFound();

            return _mapper.Map<ToolDto>(tool);
        }

        // PUT: api/Tools/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTool(long id, ToolDto toolDto)
        {
            if (id != toolDto.Id)
                return BadRequest();

            var tool = _mapper.Map<Tool>(toolDto);
            _context.Entry(tool).State = EntityState.Modified;

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
            var tool = _mapper.Map<Tool>(toolDto);

            _context.Tools.Add(tool);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostTool), toolDto);
        }

        // DELETE: api/Tools/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToolDto>> DeleteTool(long id)
        {
            var tool = await _context.Tools.FindAsync(id);
            if (tool is null)
                return NotFound();

            _context.Tools.Remove(tool);
            await _context.SaveChangesAsync();

            return _mapper.Map<ToolDto>(tool);
        }

        private bool ToolExists(long id)
        {
            return _context.Tools.Any(t => t.Id == id);
        }
    }
}
