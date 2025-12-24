using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSellingAPI.Models
{
    public class PurchaseModel
    {
        [Key]
        public int PurchaseId { get; set; }
        [Required]
        public int CarId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public double PurchasePrice { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string Status { get; set; } = "Completed";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("CarId")]
        public CarModel? Car { get; set; }

        [ForeignKey("UserId")]
        public UserModel? User { get; set; }
    }
}
