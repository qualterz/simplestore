using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Web.Areas.Administration.ViewModels
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public string CategoryName
        {
            get => Category?.Name;
            set => Category.Name = value;
        }

        public CategoryViewModel Category { get; set; }
        public List<CharacteristicViewModel> Characteristics { get; set; }

        public ItemViewModel()
        {
            Category = new();
            Characteristics = new();
        }
    }
}
