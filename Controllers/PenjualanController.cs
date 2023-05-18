using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenjualanUMKM.Context;
using PenjualanUMKM.Models;

namespace PenjualanUMKM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenjualanController : ControllerBase
    {
        private readonly DataContext _db;

        public PenjualanController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Penjualan>>> GetPenjualan()
        {
            if (_db.Penjualans == null)
            {
                return NotFound();
            }
            return await _db.Penjualans.ToListAsync();
        }

        [HttpGet("{noSales}")]
        public async Task<ActionResult<Penjualan>> GetPenjualan(string noSales)
        {
            if (_db.Penjualans == null)
            {
                return NotFound();
            }
            var Penjualan = await _db.Penjualans.FirstOrDefaultAsync(pro => pro.noSales == noSales);

            if (Penjualan == null)
            {
                return NotFound();
            }
            return Penjualan;
        }

        [HttpPost]
        public async Task<ActionResult<Penjualan>> PostPenjualan(Penjualan Penjualan)
        {
            if (Penjualan == null)
                return BadRequest(ModelState);

            var Penjualans = await _db.Penjualans.FirstOrDefaultAsync(p => p.noSales == Penjualan.noSales);

            if (Penjualans == null)
            {
                _db.Penjualans.Add(Penjualan);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPenjualan), new { kp = Penjualan.noSales }, Penjualan);
            }
            else
            {
                ModelState.AddModelError("", "Penjualan already Exist");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPut("{noSales}")]
        public async Task<IActionResult> PutPenjualan(string noSales, Penjualan Penjualan)
        {
            if (noSales != Penjualan.noSales)
            {
                return BadRequest();
            }

            _db.Entry(Penjualan).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PenjualanExist(noSales))
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

        [HttpDelete("{noSales}")]
        public async Task<IActionResult> DeletePenjualan(string noSales)
        {
            if (_db.Penjualans == null)
            {
                return NotFound();
            }

            var Penjualan = await _db.Penjualans.FindAsync(noSales);
            if (Penjualan == null)
            {
                return NotFound();
            }

            _db.Penjualans.Remove(Penjualan);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool PenjualanExist(string noSales)
        {
            return (_db.Penjualans?.Any(e => e.noSales == noSales)).GetValueOrDefault();
        }
    }
}
