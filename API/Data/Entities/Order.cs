using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal Subtotal { get; set; }

        //Navigation Properties
        public IEnumerable<OrderItem> OrderItems { get; set; }
            = Enumerable.Empty<OrderItem>();

        public AppUser AppUser { get; set; }

        public int AppUserId { get; set; }
    }

}