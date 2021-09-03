using AutoMapper;
using SimpleStore.Application.Mapper;
using SimpleStore.Application.Models;
using SimpleStore.Core.Entities;
using SimpleStore.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStore.Application.Services
{
    public interface IItemService
    {
        List<ItemModel> GetItemList();
        ItemModel GetItemById(int itemId);
        ItemModel AddItem(ItemModel itemModel);
        void DeleteItem(ItemModel itemModel);
        void UpdateItem(ItemModel itemModel);
    }

    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        private readonly IMapper mapper = ObjectMapper.Mapper;

        public List<ItemModel> GetItemList()
        {
            var itemList = itemRepository.Entities.ToList();
            return mapper.Map<List<ItemModel>>(itemList);
        }

        public ItemModel GetItemById(int itemId)
        {
            var item = itemRepository.Entities
                .SingleOrDefault(e => e.ItemId == itemId);
            return mapper.Map<ItemModel>(item);
        }

        public ItemModel AddItem(ItemModel itemModel)
        {
            var item = mapper.Map<Item>(itemModel);
            item = itemRepository.Add(item);
            return mapper.Map<ItemModel>(item);
        }

        public void DeleteItem(ItemModel itemModel)
        {
            var item = itemRepository.Entities
                .SingleOrDefault(e => e.ItemId == itemModel.ItemId);
            itemRepository.Delete(item);
        }

        public void UpdateItem(ItemModel itemModel)
        {
            var item = mapper.Map<Item>(itemModel);
            itemRepository.Update(item);
        }
    }
}
