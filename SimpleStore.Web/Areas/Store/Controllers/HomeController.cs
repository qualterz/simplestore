using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Application.Models;
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
        public enum SortType
        {
            Newest,
            Popular,
            LowPrice,
            HighPrice,
        };

        private readonly IItemControllerService itemService;

        public HomeController(IItemControllerService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult Index()
        {
            var items = itemService.GetItemList();

            return View(items);
        }

        public PartialViewResult ItemList(string search, SortType sortType)
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

            var sorted = sortType switch
            {
                SortType.Newest => items.OrderBy(e => e.ItemId),
                SortType.Popular => items.OrderBy(e => e.Ordered),
                SortType.LowPrice => items.OrderBy(e => e.Price),
                SortType.HighPrice => items.OrderByDescending(e => e.Price),
                _ => items as IOrderedEnumerable<ItemViewModel>,
            };

            items = sorted.ToList();

            return PartialView("_ItemList", items);
        }
    }
}
