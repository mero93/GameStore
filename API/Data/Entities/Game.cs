using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Entities
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get;  set; }

        public string Description { get; set; }

        public int Downloads { get; set; }

        public decimal Rating { get; set; }

        public int RatingNumber { get; set; }

        public decimal Price { get; set; }

        //Photo
        public string PublicId { get; set; }
        public string PhotoUrl { get; set; }

        //Navigation Properties
        public IEnumerable<GameCategory> Categories { get; set; } = new List<GameCategory>();

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public IEnumerable<Review> Reviews { get; set; } = new List<Review>();

        public IEnumerable<Discussion> Discussions { get; set; } = new List<Discussion>();
    }

}