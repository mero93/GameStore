using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class AppUserModel
    {
        public DateTime ExpiresAt { get; set; }
        public string PhotoUrl { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; internal set; }
        [Required]
        [MinLength(1)]
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        [Required]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Phone { get; set; }

        public string PaymentType { get; set; }

        [MaxLength(600)]
        public string OrderComment { get; set; }

        public bool AdditionalInfo { get; set; }
    }
}
