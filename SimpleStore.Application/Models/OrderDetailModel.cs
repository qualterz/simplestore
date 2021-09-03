namespace SimpleStore.Application.Models
{
    public class OrderDetailModel
    {
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public ItemModel Item { get; set; }
    }
}
