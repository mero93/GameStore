using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class RegisterUserModel
    {
        [Required]
        [StringLength(12, MinimumLength = 3)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string ConfirmPassword { get; set; }
    }
}