using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [Required]
        public override string UserName { get; set; }

        //Photo
        public string PublicId { get; set; }
        public string PhotoUrl { get; set; }

        //Navigation Properties
        public IEnumerable<Order> Orders { get; set; }

        public RefreshToken RefreshToken { get; set; }

        public IEnumerable<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();

        public IEnumerable<Review> Reviews { get; set; } = new List<Review>();

        public IEnumerable<Discussion> Discussions { get; set; } = new List<Discussion>();

        public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();

        //Additional Information
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Phone { get; set; }

        public string PaymentType { get; set; }

        public string OrderComment { get; set; }

        public bool AdditionalInfo { get; set; }
    }
}