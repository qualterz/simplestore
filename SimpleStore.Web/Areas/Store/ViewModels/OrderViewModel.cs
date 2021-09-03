using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleStore.Web.Areas.Store.ViewModels
{
    public class OrderViewModel
    {
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}
