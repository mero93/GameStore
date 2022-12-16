using API.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ReviewModel
    {
        [Required]
        [Range(0, 5)]
        public decimal Rating { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 7)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 30)]
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public int AppUserId { get; set; }
        public string Username { get; set; }
        public int GameId { get; set; }
        public string Game { get; set; }
    }
}
