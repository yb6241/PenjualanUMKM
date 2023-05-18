using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenjualanUMKM.Context;
using PenjualanUMKM.Models;

namespace PenjualanUMKM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaksiController : ControllerBase
    {
        private readonly DataContext _db;

        public TransaksiController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaksi>>> GetTransaksi()
        {
            if(_db.Transaksis == null)
            {
                return NotFound();
            }
            return await _db.Transaksis.ToListAsync();
        }

        [HttpGet("{idSales}")]
        public async Task<ActionResult<Transaksi>> GetTransaksi(string idSales)
        {
            if(_db.Transaksis == null)
            {
                return NotFound();
            }
            var Transaksi = await _db.Transaksis.FirstOrDefaultAsync(pro => pro.idSales == idSales);

            if(Transaksi == null)
            {
                return NotFound();
            }
            return Transaksi;
        }

        [HttpPost]
        public async Task<ActionResult<Transaksi>> PostTransaksi(Transaksi Transaksi)
        {
            if (Transaksi == null)
                return BadRequest(ModelState);

            var Transaksis = await _db.Transaksis.FirstOrDefaultAsync(p => p.idSales == Transaksi.idSales);

            if (Transaksis == null)
            {
                _db.Transaksis.Add(Transaksi);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTransaksi), new { kp = Transaksi.idSales }, Transaksi);
            }
            else
            {
                ModelState.AddModelError("", "Transaksi already Exist");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPut("{idSales}")]
        public async Task<IActionResult> PutTransaksi(string idSales, Transaksi Transaksi)
        {
            if (idSales != Transaksi.idSales)
            {
                return BadRequest();
            }

            _db.Entry(Transaksi).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransaksiExist(idSales))
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

        [HttpDelete("{idSales}")]
        public async Task<IActionResult> DeleteTransaksi(string idSales)
        {
            if(_db.Transaksis == null)
            {
                return NotFound();
            }

            var Transaksi = await _db.Transaksis.FindAsync(idSales);
            if(Transaksi == null)
            {
                return NotFound();
            }

            _db.Transaksis.Remove(Transaksi);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool TransaksiExist(string idSales)
        {
            return (_db.Transaksis?.Any(e => e.idSales == idSales)).GetValueOrDefault();
        }
    }
}
