using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenjualanUMKM.Context;
using PenjualanUMKM.Models;

namespace PenjualanUMKM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntrianController : ControllerBase
    {
        private readonly DataContext _db;

        public AntrianController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Antrian>>> GetAntrian()
        {
            if (_db.Antrians == null)
            {
                return NotFound();
            }
            return await _db.Antrians.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Antrian>> GetAntrian(int Id)
        {
            if (_db.Antrians == null)
            {
                return NotFound();
            }
            var Antrian = await _db.Antrians.FirstOrDefaultAsync(pro => pro.Id == Id);

            if (Antrian == null)
            {
                return NotFound();
            }
            return Antrian;
        }

        [HttpPost]
        public async Task<ActionResult<Antrian>> PostAntrian(Antrian Antrian)
        {
            if (Antrian == null)
                return BadRequest(ModelState);

            var Antrians = await _db.Antrians.FirstOrDefaultAsync(p => p.idSales == Antrian.idSales);

            if (Antrians == null)
            {
                _db.Antrians.Add(Antrian);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAntrian), new { idsales = Antrian.idSales }, Antrian);
            }
            else
            {
                ModelState.AddModelError("", "Antrian already Exist");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutAntrian(int Id, Antrian Antrian)
        {
            if (Id != Antrian.Id)
            {
                return BadRequest();
            }

            _db.Entry(Antrian).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AntrianExist(Id))
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAntrian(int Id)
        {
            if (_db.Antrians == null)
            {
                return NotFound();
            }

            var Antrian = await _db.Antrians.FindAsync(Id);
            if (Antrian == null)
            {
                return NotFound();
            }

            _db.Antrians.Remove(Antrian);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool AntrianExist(int Id)
        {
            return (_db.Antrians?.Any(e => e.Id == Id)).GetValueOrDefault();
        }
    }
}
