using System.ComponentModel.DataAnnotations;

namespace CarSellingAPI.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; } 
        [Required]
        public string Password { get; set; } 
        [Required]
        public string Email { get; set; } 
        [Required]
        [MaxLength(10)]
        public string Phone { get; set; } 
        [Required]
        public string Role {  get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
    }
}
