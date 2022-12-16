using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class TokenRequestModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
