using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreWebApi.Models;

namespace CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly PharmacyModel _context;

        public UnitController(PharmacyModel context)
        {
            _context = context;
        }

        // GET: api/Unit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnit()
        {
          if (_context.Unit == null)
          {
              return NotFound();
          }
            return await _context.Unit.ToListAsync();
        }

        // GET: api/Unit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Unit>> GetUnit(int id)
        {
          if (_context.Unit == null)
          {
              return NotFound();
          }
            var unit = await _context.Unit.FindAsync(id);

            if (unit == null)
            {
                return NotFound();
            }

            return unit;
        }

        // PUT: api/Unit/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnit(int id, Unit unit)
        {
            if (id != unit.Id)
            {
                return BadRequest();
            }

            _context.Entry(unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
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

        // POST: api/Unit
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Unit unit)
        {
          if (_context.Unit == null)
          {
              return Problem("Entity set 'PharmacyModel.Unit'  is null.");
          }
            _context.Unit.Add(unit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new { id = unit.Id }, unit);
        }

        // DELETE: api/Unit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            if (_context.Unit == null)
            {
                return NotFound();
            }
            var unit = await _context.Unit.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _context.Unit.Remove(unit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnitExists(int id)
        {
            return (_context.Unit?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
