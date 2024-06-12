using System.ComponentModel.DataAnnotations;

namespace ApiTMDT.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        
        public string Phone {  get; set; }
    }
}
