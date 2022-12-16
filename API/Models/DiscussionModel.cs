using API.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class DiscussionModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 7)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 30)]
        public string Body { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime LastActivity { get; set; }

        //Navigation
        [Required]
        public int AppUserId { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}
