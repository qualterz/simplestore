using System.Collections.Generic;

namespace SimpleStore.Application.Models
{
    public class ItemModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Ordered { get; set; }

        public CategoryModel Category { get; set; }
        public List<CharacteristicModel> Characteristics { get; set; }
    }
}
