using CarSellingAPI.Data;
using CarSellingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSellingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private readonly CarSellingDbContext _context;

        public CarImagesController(CarSellingDbContext context)
        {
            _context = context;
        }

        #region Get All Car Images

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _context.CarImages.ToListAsync();

            return Ok(images);
        }

        #endregion

        #region GetById Images

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImagesById(int id)
        {
            var images = await _context.CarImages.FindAsync(id);

            if (images == null)
                return NotFound(new { message = "Images not found" });

            return Ok(images);
        }

        #endregion

        #region Add New Car Images

        [HttpPost]
        public async Task<IActionResult> AddImages(CarImagesModel images)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            images.CreatedDate = DateTime.Now;

            _context.CarImages.Add(images);
            await _context.SaveChangesAsync();

            return Ok(images);
        }

        #endregion

        #region Update Car Images

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImages(int id, CarImagesModel images)
        {
            if (id != images.ImageId)
                return BadRequest(new { message = "Image ID mismatch" });

            var existingImages = await _context.CarImages.FindAsync(id);
            if (existingImages == null)
                return NotFound(new { message = "Image is not found" });

            existingImages.ImageId = images.ImageId;
            existingImages.CarId = images.CarId;
            existingImages.ImageUrl = images.ImageUrl;
            existingImages.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = "User Updated Successfully" });
        }

        #endregion

        #region Delete Car images

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteImages(int id)
        {
            var images = await _context.CarImages.FindAsync(id);
            if (images == null)
                return NotFound(new { message = "Image not found" });

            _context.CarImages.Remove(images);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Image Deleted Successfully" });
        }

        #endregion
    }
}
