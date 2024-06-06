using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class LoginModel
    {
        [Required]//Thuộc tính này bắt buộc phải có giá trị (không được null hoặc rỗng)
      [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
