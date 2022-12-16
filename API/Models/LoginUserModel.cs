using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class LoginUserModel
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
