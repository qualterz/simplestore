using System.Collections.Generic;

namespace SimpleStore.Web.Areas.Store.ViewModels
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Ordered { get; set; }
        public bool InCart { get; set; }
        
        public CategoryViewModel Category { get; set; }
        public List<CharacteristicViewModel> Characteristics { get; set; }
    }
}
