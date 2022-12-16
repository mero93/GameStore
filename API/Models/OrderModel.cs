using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class OrderModel
    {
        public int Id { get; set; }

        [Required]
        public int? AppUserId { get; set; }

        [Required]
        public DateTime? OrderDate { get; set; }

        [Required]
        public decimal? Subtotal { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<OrderItemModel> OrderItems { get; set; } = Enumerable.Empty<OrderItemModel>();
    }
}