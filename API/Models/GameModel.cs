using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class GameModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        public DateTime? DateAdded { get; set; }

        public string Description { get; set; }

        public int Downloads { get; set; }

        public decimal Rating { get; set; }

        public int RatingNumber { get; set; }

        public decimal Price { get; set; }

        //Photo
        public string PublicId { get; set; }
        public string PhotoUrl { get; set; }

        //Categories
        public IEnumerable<string> Categories { get; set; }

        [Required]
        public IEnumerable<int> CategoriesId { get; set; }

        //Publisher
        public string Publisher { get; set; }

        [Required]
        public int PublisherId { get; set; }
    }
}