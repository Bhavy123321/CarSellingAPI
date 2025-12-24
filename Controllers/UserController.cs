using CarSellingAPI.Data;
using CarSellingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSellingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CarSellingDbContext _context;

        public UserController(CarSellingDbContext context)
        {
            _context = context;
        }

        #region Get All Users
            
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var user = await _context.User.ToListAsync();

            return Ok(user);
        }

        #endregion

        #region GetById User

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        #endregion

        #region Add New User

        [HttpPost]
        public async Task<IActionResult> AddUser(UserModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool exists = await _context.User.AnyAsync(u => u.UserName == u.UserName);
            if (exists)
                return BadRequest(new { message = "User already exists" });

            user.CreatedDate = DateTime.Now;

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        #endregion

        #region Update User

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserModel user)
        {
            if (id != user.UserId)
                return BadRequest(new { message = "User ID mismatch" });

            var existingUser = await _context.User.FindAsync(id);
            if (existingUser == null)
                return NotFound(new { message = "User not found" });

            existingUser.UserId = user.UserId;
            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;
            existingUser.Role = user.Role;
            existingUser.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = "User Updated Successfully" });
        }

        #endregion

        #region Delete User

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User Deleted Successfully" });
        }

        #endregion
    }
}
