using System.Data;
using CarSellingAPI.Data;
using CarSellingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CarSellingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarSellingDbContext _context;

        public CarsController(CarSellingDbContext context)
        {
            _context = context;
        }

        #region Get All Cars

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _context.Car.ToListAsync();

            return Ok(cars);
        }

        #endregion

        #region GetById Cars

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car = await _context.Car.FindAsync(id);

            if (car == null)
                return NotFound(new { message = "Car not found" });

            return Ok(car);
        }

        #endregion

        #region Add New Car

        [HttpPost]
        public async Task<IActionResult> AddCar(CarModel car)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool exists = await _context.Car.AnyAsync(c => c.Title == car.Title);
            if (exists)
                return BadRequest(new { message = "Car already exists" });

            car.CreatedDate = DateTime.Now;

            _context.Car.Add(car);
            await _context.SaveChangesAsync();

            return Ok(car);
        }

        #endregion

        #region Update Car

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, CarModel car)
        {
            if (id != car.CarId)
                return BadRequest(new { message = "Car ID mismatch" });

            var existingCar = await _context.Car.FindAsync(id);
            if (existingCar == null)
                return NotFound(new { message = "Car not found" });

            existingCar.UserId = car.UserId;
            existingCar.BrandId = car.BrandId;
            existingCar.StatusId = car.StatusId;
            existingCar.Title = car.Title;
            existingCar.Model = car.Model;
            existingCar.Year = car.Year;
            existingCar.Price = car.Price;
            existingCar.Mileage = car.Mileage;
            existingCar.FuelType = car.FuelType;
            existingCar.Transmission = car.Transmission;
            existingCar.Description = car.Description;
            existingCar.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Car Updated Successfully" });
        }

        #endregion

        #region Delete Car

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
                return NotFound(new { message = "Car not found" });

            _context.Car.Remove(car);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Car Deleted Successfully" });
        }   
        #endregion
    }
}
