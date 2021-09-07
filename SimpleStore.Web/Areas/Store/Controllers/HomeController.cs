using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Application.Models;
using SimpleStore.Application.Services;
using SimpleStore.Web.Areas.Store.Services;
using SimpleStore.Web.Areas.Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleStore.Web.Areas.Store.Controllers
{
    [Area("Store")]
    public class HomeController : Controller
    {
        private readonly IItemService itemService;
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public HomeController(
            IItemService itemService,
            ICartService cartService,
            IMapper mapper)
        {
            this.itemService = itemService;
            this.cartService = cartService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var items = itemService.GetItemList();
            var itemViewModels = mapper.Map<List<ItemViewModel>>(items);

            var cartItems = cartService.GetItems();
            itemViewModels.ForEach(
                e => e.InCart = cartItems.Any(c => c.Item.ItemId == e.ItemId));

            return View(itemViewModels);
        }

        public PartialViewResult ItemList(string search)
        {
            var keywords = search?.Trim().ToLower().Split();
            var items = itemService.GetItemList();

            if (!string.IsNullOrEmpty(search))
            {
                var foundByCharacteristics = items.Where(
                    e => e.Characteristics.Any(
                        e => keywords.Any(
                            k => e.Value.ToLower().Contains(k))));

                var foundByName = items.Where(
                    e => keywords.Any(
                        k => e.Name.ToLower().Contains(k)));

                if (keywords.Length > 1)
                {
                    items = foundByCharacteristics
                        .Where(
                            e => foundByName
                                .Any(n => n.Name == e.Name))
                        .ToList();
                }
                else
                {
                    items = foundByName
                        .Union(foundByCharacteristics)
                        .ToList();
                }
            }

            var itemViewModels = mapper.Map<List<ItemViewModel>>(items);

            var cartItems = cartService.GetItems();
            itemViewModels.ForEach(
                e => e.InCart = cartItems.Any(c => c.Item.ItemId == e.ItemId));

            return PartialView("_ItemList", itemViewModels);
        }
    }
}
