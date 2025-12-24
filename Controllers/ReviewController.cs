using CarSellingAPI.Data;
using CarSellingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSellingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly CarSellingDbContext _context;

        public ReviewController(CarSellingDbContext context)
        {
            _context = context;
        }

        #region Get All Review

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var review = await _context.Review.ToListAsync();
            return Ok(review);
        }

        #endregion

        #region Get Review By ID

        [HttpGet("id")]
        public async Task<IActionResult> GetReviewByID(int id)
        {
            var reivew = await _context.Review.FindAsync(id);

            if(reivew == null)
            {
                return BadRequest(new { message = "Review Not Found" });
            }
            return Ok(reivew);
        }

        #endregion

        #region Delete Review

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);

            if (review == null)
            {
                return BadRequest(new { message = "Review Not Found" });
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Review Deleted Successfully"});

        }

        #endregion

        #region Add New Review

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewModel review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            review.CreatedDate = DateTime.Now;

            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);

        }

        #endregion

        #region Update Review


        [HttpPut("id")]
        public async Task<IActionResult> UpdateReview(ReviewModel review,int id)
        {
            if (id != review.UserId)
                return BadRequest(new { message = "Review ID mismatch" });

            var existingReview = await _context.Review.FindAsync(id);
            if (existingReview == null)
                return NotFound(new { message = "Review not found" });

            existingReview.ReviewId = review.ReviewId;
            existingReview.CarId = review.CarId;
            existingReview.UserId = review.UserId;
            existingReview.Rating = review.Rating;
            existingReview.Comment = review.Comment;
            existingReview.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Review Updated Successfully" });
        }

        #endregion
    }
}
