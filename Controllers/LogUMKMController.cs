using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenjualanUMKM.Context;
using PenjualanUMKM.Models;

namespace PenjualanUMKM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogUMKMController : ControllerBase
    {
        private readonly DataContext _db;

        public LogUMKMController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogUMKM>>> GetLogUMKM()
        {
            if (_db.LogUMKMs == null)
            {
                return NotFound();
            }
            return await _db.LogUMKMs.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<LogUMKM>> PostLogUMKM(LogUMKM logumkm)
        {
            _db.LogUMKMs.Add(logumkm);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLogUMKM), new { id = logumkm.Id }, logumkm);
        }
    }
}
