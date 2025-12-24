using CarSellingAPI.Data;
using CarSellingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSellingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandsController : ControllerBase
    {
        private readonly CarSellingDbContext _context;

        public CarBrandsController(CarSellingDbContext context)
        {
            _context = context;
        }

        #region GetAll Car Brands
        [HttpGet]
        public async Task<IActionResult> GetAllCarBrand()
        {
            var carBrands = await _context.CarBrand.ToListAsync();

            return Ok(carBrands);
        }

        #endregion

        #region Get Car Brand By ID

        [HttpGet("id")]
        public async Task<IActionResult> GetCarBrandById(int id)
        {
            var carBrand  = await _context.CarBrand.FindAsync(id);

            if(carBrand == null)
            {
                return BadRequest(new {message = "Car Brand Not Found"});
            }
            return Ok(carBrand);
        }

        #endregion

        #region Add Car Brand

        [HttpPost]
        public async Task<IActionResult> AddCarBrand(CarBrandsModel carBrand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            carBrand.CreatedDate = DateTime.Now;

            _context.CarBrand.Add(carBrand);
            await _context.SaveChangesAsync();

            return Ok(carBrand);
        }

        #endregion

        #region Delete Car Brand

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCarBrand(int id)
        {
            var carBrand = await _context.CarBrand.FindAsync(id);
            if (carBrand == null)
                return NotFound(new { message = "Car Brand not found" });

            _context.CarBrand.Remove(carBrand);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Car Brand Deleted Successfully" });
        }

        #endregion

        #region Update Car Brand

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarBrand(int id, CarBrandsModel carBrand)
        {
            if (id != carBrand.BrandId)
                return BadRequest(new { message = "Car Brand ID mismatch" });

            var existingCarBrand= await _context.CarBrand.FindAsync(id);
            if (existingCarBrand == null)
                return NotFound(new { message = "Car Brand not found" });

            existingCarBrand.BrandId = carBrand.BrandId;
            existingCarBrand.BrandName = carBrand.BrandName;
            existingCarBrand.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Car Brand Updated Successfully" });
        }

        #endregion
    }
}
