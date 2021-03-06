using System;
using System.Collections.Generic;

namespace SimpleStore.Web.Areas.Administration.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<OrderDetailViewModel> Details { get; set; }

        public OrderViewModel()
        {
            Details = new();
        }
    }
}
