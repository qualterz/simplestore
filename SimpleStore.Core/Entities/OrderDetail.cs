namespace SimpleStore.Core.Entities
{
    public class OrderDetail : Entity
    {
        public int OrderDetailId { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
