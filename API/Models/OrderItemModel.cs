using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class OrderItemModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 100000)]
        public decimal? Price { get; set; }

        [Required]
        public int? OrderId { get; set; }

        [Required]
        public int? GameId { get; set; }

        [Required]
        public string Game { get; set; }
    }
}
