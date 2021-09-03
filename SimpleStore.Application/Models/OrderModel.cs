using System;
using System.Collections.Generic;

namespace SimpleStore.Application.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<OrderDetailModel> Details { get; set; }
    }
}
