using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Entities
{
    public class OrderItem
    {
        [Required]
        public int Quantity { get; set; }

        //Navigation Properties
        public int GameId { get; set; }

        public Game Game { get; set; }

        public decimal Price { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}