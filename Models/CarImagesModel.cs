using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarSellingAPI.Models
{
    public class CarImagesModel
    {
        [Key]
        public int ImageId { get; set; }
        [Required]
        public int CarId { get; set; }

        public string ImageUrl { get; set; }    
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey("CarId")]
        public CarModel? Car {  get; set; }
    }
}
