using System.ComponentModel.DataAnnotations;

namespace CarSellingAPI.Models
{
    public class CarStatusModel
    {
        [Key]
        public int StatusId { get; set; }
        [Required]
        public string StatusName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
}
