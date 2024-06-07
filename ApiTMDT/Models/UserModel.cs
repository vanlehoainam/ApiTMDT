using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class UserModel
    {
        [Key]
        public int IdUser { get; set; }

        [Required]//Thuộc tính này bắt buộc phải có giá trị (không được null hoặc rỗng)
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [MaxLength(10)]
        public string Phone { get; set; } // Changed to string
    }
}
