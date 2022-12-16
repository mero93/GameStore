using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(1500, MinimumLength = 10)]
        public string Body { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        [Required]
        public int AppUserId { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        [Required]
        public int DiscussionId { get; set; }
        public int? CommentId { get; set; }

        public IEnumerable<CommentModel> Replies { get; set; } = new List<CommentModel>();
    }
}
