using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

            try
            {
                string StoredProc = "exec Usp_GetData " + "@tablename = Produk";
                return await _db.Produks.FromSqlRaw(StoredProc).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{kodeProduk}")]
        public async Task<ActionResult<Produk>> GetProduk(string kodeProduk)
        {
            if(_db.Produks == null)
            {
                return NotFound();
            }

            try
            {
                string StoredProc = "exec Usp_GetDatabyId " + "@tablename = 'Produk', @id= '" + kodeProduk + "'";
                var produk = await _db.Produks.FromSqlRaw(StoredProc).ToListAsync();

                if (produk == null)
                {
                    return NotFound();
                }
                return produk.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Produk>> PostProduk(Produk produk)
        {
            if (produk == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!ProdukExist(produk.kodeProduk))
                {
                    var parameter = new List<SqlParameter>();
                    parameter.Add(new SqlParameter("@kodeProduk", produk.kodeProduk));
                    parameter.Add(new SqlParameter("@namaProduk", produk.namaProduk));
                    parameter.Add(new SqlParameter("@hargaProduk", produk.hargaProduk));

                    var result = await Task.Run(() => _db.Database
                   .ExecuteSqlRawAsync(@"exec Usp_InsertProduk @kodeProduk, @namaProduk, @hargaProduk", parameter.ToArray()));

                    return CreatedAtAction(nameof(GetProduk), new { kp = produk.kodeProduk }, produk);
                }
                else
                {
                    ModelState.AddModelError("", "Produk already Exist");
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                if (!ProdukExist(produk.kodeProduk))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
        }

        [HttpPut("{kodeProduk}")]
        public async Task<IActionResult> PutProduk(string kodeProduk, Produk produk)
        {
            if (kodeProduk != produk.kodeProduk)
            {
                return BadRequest();
            }

            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(new SqlParameter("@kodeProduk", produk.kodeProduk));
                parameter.Add(new SqlParameter("@namaProduk", produk.namaProduk));
                parameter.Add(new SqlParameter("@hargaProduk", produk.hargaProduk));

                var result = await Task.Run(() => _db.Database
                .ExecuteSqlRawAsync(@"exec Usp_UpdateProduk @kodeProduk, @namaProduk, @hargaProduk", parameter.ToArray()));

                return CreatedAtAction(nameof(GetProduk), new { kp = produk.kodeProduk }, produk);
            }
            catch (Exception ex)
            {
                if (!ProdukExist(kodeProduk))
                {
                    return NotFound();
                }
                else
                {
                    throw ex;
                }
            }
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

            try
            {
                _db.Produks.Remove(produk);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

        private bool ProdukExist(string kodeProduk)
        {
            return (_db.Produks?.Any(e => e.kodeProduk == kodeProduk)).GetValueOrDefault();
        }
    }
}
