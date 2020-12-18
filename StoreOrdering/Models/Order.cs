namespace StoreOrdering.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CreatorId { get; set; }
        public MockUserIdentity Creator { get; set; }
        public int? CartId { get; set; }
        public Cart Cart { get; set; }
    }
}