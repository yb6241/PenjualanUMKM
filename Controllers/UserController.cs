using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PenjualanUMKM.Context;
using PenjualanUMKM.Models;
using System.Security.Cryptography;
using System.Text;

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
                user.password = MD5Encryption(user.password);//encrypt md5
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

            user.password = MD5Encryption(user.password);//encrypt md5
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

        public static string MD5Encryption(string encryptionText)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] array = Encoding.UTF8.GetBytes(encryptionText);
            array = md5.ComputeHash(array);
            StringBuilder sb = new StringBuilder();

            foreach (byte ba in array)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        [HttpPost, Route("[action]", Name = "Login")]
        public async Task<ActionResult<User>> Login(string username, string password)
        {
            if (_db.Users == null)
            {
                return NotFound();
            }

            var user = await _db.Users.FirstOrDefaultAsync(pro => pro.username == username && pro.password == MD5Encryption(password));

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}
