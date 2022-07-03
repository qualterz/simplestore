using AutoMapper;
using SimpleStore.Application.Services;
using SimpleStore.Web.Areas.Store.ViewModels;
using System.Collections.Generic;

namespace SimpleStore.Web.Areas.Store.Services
{
    public interface IItemControllerService
    {
        List<ItemViewModel> GetItemList();
        ItemViewModel GetItemById(int itemId);
    }

    public class ItemControllerService : IItemControllerService
    {
        private readonly IItemService itemService;
        private readonly ICartControllerService cartService;
        private readonly IMapper mapper;

        public ItemControllerService(
            IItemService itemService,
            ICartControllerService cartService,
            IMapper mapper)
        {
            this.itemService = itemService;
            this.cartService = cartService;
            this.mapper = mapper;
        }

        public ItemViewModel GetItemById(int itemId)
        {
            var item = itemService.GetItemById(itemId);
            var mapped = mapper.Map<ItemViewModel>(item);
            mapped.InCart = cartService.Contains(itemId);
            return mapped;
        }

        public List<ItemViewModel> GetItemList()
        {
            var items = itemService.GetItemList();
            var mapped = mapper.Map<List<ItemViewModel>>(items);
            mapped.ForEach(e => e.InCart = cartService.Contains(e.ItemId));
            return mapped;
        }
    }
}
