using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class AuthorizationModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public DateTime ExpiresAt { get; set; }
    }
}
