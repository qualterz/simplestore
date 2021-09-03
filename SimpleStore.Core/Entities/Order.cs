using System;
using System.Collections.Generic;

namespace SimpleStore.Core.Entities
{
    public class Order : Entity
    {
        public int OrderId { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual IList<OrderDetail> Details { get; set; }
    }
}
