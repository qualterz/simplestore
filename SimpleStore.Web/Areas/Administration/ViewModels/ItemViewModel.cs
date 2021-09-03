using AutoMapper;
using System.Collections.Generic;

namespace SimpleStore.Web.Areas.Administration.ViewModels
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<CharacteristicViewModel> Characteristics { get; set; }

        public ItemViewModel()
        {
            Characteristics = new();
        }
    }
}
