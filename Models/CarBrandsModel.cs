using System.ComponentModel.DataAnnotations;

namespace CarSellingAPI.Models
{
    public class CarBrandsModel
    {
        [Key]
        public int BrandId { get; set; }
        [Required]
        public string BrandName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? ModifiedDate { get; set; }

    }
}
