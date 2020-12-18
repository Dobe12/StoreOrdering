using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreOrdering.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ItemId { get; set; }

        public Cart Cart { get; set; }
        public Item Item { get; set; }
    }
}
