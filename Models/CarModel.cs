using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CarSellingAPI.Models
{
    public class CarModel
    {
        [Key]
        public int CarId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BrandId { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required,MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public string Model { get; set; }
        public int? Year {  get; set; }
        [Required]
        public double Price { get; set; }
        public int? Mileage { get; set; }
        [Required]
        public string FuelType { get; set; }
        [Required]
        public string Transmission { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("UserId")]
        public UserModel? User { get; set; }

        [ForeignKey("BrandId")]
        public CarBrandsModel? Brand { get; set; }

        [ForeignKey("StatusId")]
        public CarStatusModel? Status { get; set; }
    }
}
