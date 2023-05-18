using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenjualanUMKM.Context;
using PenjualanUMKM.Models;

namespace PenjualanUMKM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdukController : ControllerBase
    {
        private readonly DataContext _db;

        public ProdukController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produk>>> GetProduk()
        {
            if(_db.Produks == null)
            {
                return NotFound();
            }
            return await _db.Produks.ToListAsync();
        }

        [HttpGet("{kodeProduk}")]
        public async Task<ActionResult<Produk>> GetProduk(string kodeProduk)
        {
            if(_db.Produks == null)
            {
                return NotFound();
            }
            var produk = await _db.Produks.FirstOrDefaultAsync(pro => pro.kodeProduk == kodeProduk);

            if(produk == null)
            {
                return NotFound();
            }
            return produk;
        }

        [HttpPost]
        public async Task<ActionResult<Produk>> PostProduk(Produk produk)
        {
            if (produk == null)
                return BadRequest(ModelState);

            var produks = await _db.Produks.FirstOrDefaultAsync(p => p.kodeProduk == produk.kodeProduk);

            if (produks == null)
            {
                _db.Produks.Add(produk);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduk), new { kp = produk.kodeProduk }, produk);
            }
            else
            {
                ModelState.AddModelError("", "Produk already Exist");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPut("{kodeProduk}")]
        public async Task<IActionResult> PutProduk(string kodeProduk, Produk produk)
        {
            if (kodeProduk != produk.kodeProduk)
            {
                return BadRequest();
            }

            _db.Entry(produk).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdukExist(kodeProduk))
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

        [HttpDelete("{kodeProduk}")]
        public async Task<IActionResult> DeleteProduk(string kodeProduk)
        {
            if(_db.Produks == null)
            {
                return NotFound();
            }

            var produk = await _db.Produks.FindAsync(kodeProduk);
            if(produk == null)
            {
                return NotFound();
            }

            _db.Produks.Remove(produk);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdukExist(string kodeProduk)
        {
            return (_db.Produks?.Any(e => e.kodeProduk == kodeProduk)).GetValueOrDefault();
        }
    }
}
