using CarSellingAPI.Data;
using CarSellingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSellingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarStatusController : ControllerBase
    {
        private readonly CarSellingDbContext _context;

        public CarStatusController(CarSellingDbContext context)
        {
            _context = context;
        }

        #region GetAll Car Status

        [HttpGet]

        public async Task<IActionResult> GetAllCarStatus()
        {
            var carStatus = await _context.CarStatus.ToListAsync();

            return Ok(carStatus);
        }

        #endregion

        #region Get Car Status By ID

        [HttpGet("id")]
        public async Task<IActionResult> GetstausById(int id)
        {
            var carStatus = await _context.CarStatus.FindAsync(id);

            if (carStatus == null)
            {
                return BadRequest(new {message = "Car Status Not Found"});
            }
            return Ok(carStatus);
        }

        #endregion

        #region

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCarStatus(int id)
        {
            var carStatus = _context.CarStatus.Find(id);

            if (carStatus == null)
            {
                return BadRequest(new { message = "Car Brand Not Found" });
            }
            _context.CarStatus.Remove(carStatus);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Car Status Deleted Successfully" });
        }

        #endregion

        #region Add New Car Status

        [HttpPost]
        public async Task<IActionResult> AddCarStatus(CarStatusModel carStatus)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool exists = await _context.CarStatus.AnyAsync(cs => cs.StatusName == cs.StatusName);
            if (exists)
                return BadRequest(new { message = "Car Status already exists" });

            carStatus.CreatedDate = DateTime.Now;

            _context.CarStatus.Add(carStatus);
            await _context.SaveChangesAsync();

            return Ok(carStatus);
        }

        #endregion

        #region Update Car Status


        [HttpPut("id")]
        public async Task<IActionResult> UpdateCarStatus(CarStatusModel carStatus, int id)
        {
            if (id != carStatus.StatusId)
                return BadRequest(new { message = "Status ID mismatch" });

            var existingCarStatus = await _context.CarStatus.FindAsync(id);
            if (existingCarStatus == null)
                return NotFound(new { message = "Car Status  not found" });

            existingCarStatus.StatusId = carStatus.StatusId;
            existingCarStatus.StatusName = carStatus.StatusName;
            existingCarStatus.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Car Status Updated Successfully" });
        }

        #endregion

    }
}
