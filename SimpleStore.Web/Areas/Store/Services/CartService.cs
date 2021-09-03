using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SimpleStore.Web.Areas.Store.ViewModels;
using SimpleStore.Application.Services;

namespace SimpleStore.Web.Areas.Store.Services
{
    public interface ICartService
    {
        public List<CartItemViewModel> GetItems();

        public void Add(int id);
        public void Remove(int id);
        public void Update(int id, int quantity);
        public bool Contains(int id);
        void ClearCart();
    }

    public class CartService : ICartService
    {
        private readonly IMapper mapper;
        private readonly ISession session;
        private readonly IItemService itemService;

        private readonly List<CartItemViewModel> items;

        public CartService(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IItemService itemService)
        {
            this.mapper = mapper;

            session = httpContextAccessor.HttpContext.Session;

            this.itemService = itemService;

            items = GetItemsFromSession();
        }

        public List<CartItemViewModel> GetItems()
        {
            return items;
        }

        private List<CartItemViewModel> GetItemsFromSession()
        {
            return session.GetObjectFromJson<List<CartItemViewModel>>("CartItems") ?? new();
        }

        private void SetItemsToSession()
        {
            session.SetObjectAsJson("CartItems", items);
        }

        public void Add(int id)
        {
            var item = itemService.GetItemById(id);

            var itemViewModel = mapper.Map<ItemViewModel>(item);

            var cartItem = new CartItemViewModel()
            {
                Item = itemViewModel,
                Quantity = 1,
            };

            items.Add(cartItem);
            SetItemsToSession();
        }

        public void Remove(int id)
        {
            var item = items.FirstOrDefault(
                e => e.Item.ItemId == id
            );
            items.Remove(item);
            SetItemsToSession();
        }

        public void Update(int id, int quantity)
        {
            var updatedItem = items.Find(e => e.Item.ItemId == id);
            updatedItem.Quantity = quantity;
            SetItemsToSession();
        }

        public bool Contains(int id)
        {
            return items.Any(e => e.Item.ItemId == id);
        }

        public void ClearCart()
        {
            items.Clear();
            SetItemsToSession();
        }
    }
}
