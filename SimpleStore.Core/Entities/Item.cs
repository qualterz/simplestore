using System.Collections.Generic;

namespace SimpleStore.Core.Entities
{
    public class Item : Entity
    {
        public int ItemId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual Category Category { get; set; }
        public virtual IList<ItemCharacteristic> ItemCharacteristics { get; set; }
    }
}
