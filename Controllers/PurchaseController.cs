using CarSellingAPI.Data;
using CarSellingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSellingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly CarSellingDbContext _context;

        public PurchaseController(CarSellingDbContext context)
        {
            _context = context;
        }

        #region GetAllPurcase


        [HttpGet]
        public async Task<IActionResult> GetAllPurcase()
        {
            var purchase = await _context.User.ToListAsync();

            return Ok(purchase);
        }

        #endregion

        #region GetById Purchase

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseById(int id)
        {
            var purchase = await _context.Purchase.FindAsync(id);

            if (purchase == null)
                return NotFound(new { message = "Purchase not found" });

            return Ok(purchase);
        }

        #endregion

        #region Add New Purchase

        [HttpPost]
        public async Task<IActionResult> AddPurchase(PurchaseModel purchase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            purchase.CreatedDate = DateTime.Now;

            _context.Purchase.Add(purchase);
            await _context.SaveChangesAsync();

            return Ok(purchase);
        }

        #endregion

        #region Update Purchase

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePurchase(int id, PurchaseModel purchase)
        {
            if (id != purchase.PurchaseId)
                return BadRequest(new { message = "Purchase ID mismatch" });

            var existingPurchase = await _context.Purchase.FindAsync(id);
            if (existingPurchase == null)
                return NotFound(new { message = "Purchase not found" });

            existingPurchase.PurchaseId = purchase.PurchaseId;
            existingPurchase.CarId = purchase.CarId;
            existingPurchase.UserId = purchase.UserId;
            existingPurchase.PurchasePrice = purchase.PurchasePrice;
            existingPurchase.PaymentMethod = purchase.PaymentMethod;
            existingPurchase.Status = purchase.Status;
            existingPurchase.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { message = "User Updated Successfully" });
        }

        #endregion

        #region Delete Purchase

        [HttpDelete("id")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await _context.Purchase.FindAsync(id);
            if (purchase == null)
                return NotFound(new { message = "Purchase not found" });

            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Purchase Deleted Successfully" });
        }
        #endregion
    }
}
