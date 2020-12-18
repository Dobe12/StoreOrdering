using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreOrdering.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public virtual IEnumerable<Item> Items { get; set; }


        public int CreatorId { get; set; }
        public MockUserIdentity Creator { get; set; }
    }
}
