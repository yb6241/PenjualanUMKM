using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenjualanUMKM.Context;
using PenjualanUMKM.Models;

namespace PenjualanUMKM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PelangganController : ControllerBase
    {
        private readonly DataContext _db;

        public PelangganController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pelanggan>>> GetPelanggan()
        {
            if (_db.Pelanggans == null)
            {
                return NotFound();
            }
            return await _db.Pelanggans.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Pelanggan>> PostPelanggan(Pelanggan pelanggan)
        {
            _db.Pelanggans.Add(pelanggan);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPelanggan), new { id = pelanggan.Id }, pelanggan);
        }
    }
}
