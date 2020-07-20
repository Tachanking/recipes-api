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
    public class MeasurementsController : ControllerBase
    {
        private readonly RecipesContext _context;
        private readonly IMapper _mapper;

        public MeasurementsController(RecipesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Measurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeasurementDto>>> GetMeasurements()
        {
            return await _context.Measurements.Select(m => _mapper.Map<MeasurementDto>(m)).ToListAsync();
        }

        // GET: api/Measurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeasurementDto>> GetMeasurement(long id)
        {
            var measurement = await _context.Measurements.FindAsync(id);

            if (measurement is null)
                return NotFound();

            return _mapper.Map<MeasurementDto>(measurement);
        }

        // PUT: api/Measurements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurement(long id, MeasurementDto measurementDto)
        {
            if (id != measurementDto.Id)
                return BadRequest();

            var measurement = _mapper.Map<Measurement>(measurementDto);
            _context.Entry(measurement).State = EntityState.Modified;

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
            var measurement = _mapper.Map<Measurement>(measurementDto);

            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostMeasurement), measurementDto);
        }

        // DELETE: api/Measurements/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MeasurementDto>> DeleteMeasurement(long id)
        {
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement is null)
                return NotFound();

            _context.Measurements.Remove(measurement);
            await _context.SaveChangesAsync();

            return _mapper.Map<MeasurementDto>(measurement);
        }

        private bool MeasurementExists(long id)
        {
            return _context.Measurements.Any(e => e.Id == id);
        }
    }
}
