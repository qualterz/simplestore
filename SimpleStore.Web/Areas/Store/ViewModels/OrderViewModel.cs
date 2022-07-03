using System.Collections.Generic;

namespace SimpleStore.Web.Areas.Store.ViewModels
{
    public class OrderViewModel
    {
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
