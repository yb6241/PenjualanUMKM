using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenjualanUMKM.Context;
using PenjualanUMKM.Models;

namespace PenjualanUMKM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _db;

        public UserController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            if (_db.Users == null)
            {
                return NotFound();
            }
            return await _db.Users.ToListAsync();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetUser(int Id)
        {
            if (_db.Users == null)
            {
                return NotFound();
            }
            var user = await _db.Users.FirstOrDefaultAsync(pro => pro.Id == Id);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (user == null)
                return BadRequest(ModelState);

            var users = await _db.Users.FirstOrDefaultAsync(p => p.Id == user.Id);

            if (users == null)
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUser), new { kp = user.Id }, user);
            }
            else
            {
                ModelState.AddModelError("", "User already Exist");
                return StatusCode(500, ModelState);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutUser(int Id, User user)
        {
            if (Id != user.Id)
            {
                return BadRequest();
            }

            _db.Entry(user).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExist(Id))
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
        public async Task<IActionResult> DeleteUser(int Id)
        {
            if (_db.Users == null)
            {
                return NotFound();
            }

            var user = await _db.Users.FindAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExist(int Id)
        {
            return (_db.Users?.Any(e => e.Id == Id)).GetValueOrDefault();
        }
    }
}
