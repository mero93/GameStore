using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class DiscussionDetailModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 7)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 30)]
        public string Body { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? UpdateCreated { get; set; }
        public DateTime LastActivity { get; set; }

        //Navigation
        [Required]
        public int AppUserId { get; set; }
        public string Username { get; set; }
        public string PhotoUrl { get; set; }
        [Required]
        public int GameId { get; set; }
        
        public IEnumerable<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }
}