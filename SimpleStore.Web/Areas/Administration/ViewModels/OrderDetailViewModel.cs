namespace SimpleStore.Web.Areas.Administration.ViewModels
{
    public class OrderDetailViewModel
    {
        public string ItemName
        {
            get => Item.Name;
            set => Item.Name = value;
        }

        public int Quantity { get; set; }

        public ItemViewModel Item { get; set; }

        public OrderDetailViewModel()
        {
            Item = new();
        }
    }
}
