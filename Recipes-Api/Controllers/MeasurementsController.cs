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
    public class MeasurementsController : ControllerBase
    {
        private readonly RecipesContext _context;

        public MeasurementsController(RecipesContext context)
        {
            _context = context;
        }

        // GET: api/Measurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementDto>>> GetMeasurements()
        {
            return await _context.Measurements.Select(m => MeasurementToDto(m)).ToListAsync();
        }

        // GET: api/Measurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementDto>> GetMeasurement(int id)
        {
            var measurement = await _context.Measurements.FindAsync(id);

            if (measurement is null)
            {
                return NotFound();
            }

            return MeasurementToDto(measurement);
        }

        // PUT: api/Measurements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurement(int id, MeasurementDto measurementDto)
        {
            if (id != measurementDto.Id)
            {
                return BadRequest();
            }

            _context.Entry(measurementDto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasurementExists(id))
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

        // POST: api/Measurements
        [HttpPost]
        public async Task<ActionResult<MeasurementDto>> PostMeasurement(MeasurementDto measurementDto)
        {
            var measurement = new Measurement
            {
                Name = measurementDto.Name,
                Symbol = measurementDto.Symbol
            };

            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostMeasurement), measurementDto);
        }

        // DELETE: api/Measurements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MeasurementDto>> DeleteMeasurement(int id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement is null)
            {
                return NotFound();
            }

            _context.Measurements.Remove(measurement);
            await _context.SaveChangesAsync();

            return MeasurementToDto(measurement);
        }

        private bool MeasurementExists(int id)
        {
            return _context.Measurements.Any(e => e.Id == id);
        }

        private static MeasurementDto MeasurementToDto(Measurement measurement)
        {
            return new MeasurementDto
            {
                Id = measurement.Id,
                Name = measurement.Name,
                Symbol = measurement.Symbol
            };
        }
    }
}
