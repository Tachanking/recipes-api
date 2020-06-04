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
    public class OwnedIngredientsController : ControllerBase
    {
        private readonly RecipesContext _context;

        public OwnedIngredientsController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/OwnedIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnedIngredient>>> GetOwnedIngredient()
        {
            return await _context.OwnedIngredient.ToListAsync();
        }

        // GET: api/OwnedIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnedIngredient>> GetOwnedIngredient(int id)
        {
            var ownedIngredient = await _context.OwnedIngredient.FindAsync(id);

            if (ownedIngredient == null)
            {
                return NotFound();
            }

            return ownedIngredient;
        }

        // PUT: api/OwnedIngredients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwnedIngredient(int id, OwnedIngredient ownedIngredient)
        {
            if (id != ownedIngredient.Id)
            {
                return BadRequest();
            }

            _context.Entry(ownedIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnedIngredientExists(id))
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

        // POST: api/OwnedIngredients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OwnedIngredient>> PostOwnedIngredient(OwnedIngredient ownedIngredient)
        {
            _context.OwnedIngredient.Add(ownedIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostOwnedIngredient), new { id = ownedIngredient.Id }, ownedIngredient);
        }

        // DELETE: api/OwnedIngredients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OwnedIngredient>> DeleteOwnedIngredient(int id)
        {
            var ownedIngredient = await _context.OwnedIngredient.FindAsync(id);
            if (ownedIngredient == null)
            {
                return NotFound();
            }

            _context.OwnedIngredient.Remove(ownedIngredient);
            await _context.SaveChangesAsync();

            return ownedIngredient;
        }

        private bool OwnedIngredientExists(int id)
        {
            return _context.OwnedIngredient.Any(e => e.Id == id);
        }
    }
}
