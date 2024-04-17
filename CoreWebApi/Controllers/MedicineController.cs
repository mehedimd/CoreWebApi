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
    public class MedicineController : ControllerBase
    {
        private readonly PharmacyModel _context;

        public MedicineController(PharmacyModel context)
        {
            _context = context;
        }

        // GET: api/Medicine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicine()
        {
          if (_context.Medicine == null)
          {
              return NotFound();
          }
            return await _context.Medicine.Include("Unit").ToListAsync();
        }

        // GET: api/Medicine/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicine(int id)
        {
          if (_context.Medicine == null)
          {
              return NotFound();
          }
            var medicine = await _context.Medicine.FindAsync(id);

            if (medicine == null)
            {
                return NotFound();
            }

            return medicine;
        }

        // PUT: api/Medicine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine(int id, Medicine medicine)
        {
            if (id != medicine.ID)
            {
                return BadRequest();
            }

            _context.Entry(medicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
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

        // POST: api/Medicine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine(Medicine medicine)
        {
          if (_context.Medicine == null)
          {
              return Problem("Entity set 'PharmacyModel.Medicine'  is null.");
          }
            _context.Medicine.Add(medicine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicine", new { id = medicine.ID }, medicine);
        }

        // DELETE: api/Medicine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            if (_context.Medicine == null)
            {
                return NotFound();
            }
            var medicine = await _context.Medicine.FindAsync(id);
            if (medicine == null)
            {
                return NotFound();
            }

            _context.Medicine.Remove(medicine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicineExists(int id)
        {
            return (_context.Medicine?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
