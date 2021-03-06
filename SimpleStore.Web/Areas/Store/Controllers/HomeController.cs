using Microsoft.AspNetCore.Mvc;
using SimpleStore.Web.Areas.Store.Services;
using System.Linq;

namespace SimpleStore.Web.Areas.Store.Controllers
{
    [Area("Store")]
    public class HomeController : Controller
    {
        public enum OrderType
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
            var items = itemService.GetItemList()
                .OrderByDescending(e => e.ItemId)
                .ToList();

            return View(items);
        }

        public PartialViewResult ItemList(string search, OrderType orderType)
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

                var foundByCategory = items.Where(
                    e => keywords.Any(
                        k => e.Category.Name.ToLower().Contains(k)));

                if (keywords.Length > 1)
                {
                    items = foundByName.ToList();

                    items.AddRange(foundByCharacteristics.Except(foundByName));

                    if (foundByCategory.Any())
                    {
                        items.RemoveAll(
                            e => !foundByCategory
                                .Any(c => c.Category.Name == e.Category.Name));
                    }
                }
                else
                {
                    items = foundByName
                        .Union(foundByCharacteristics)
                        .Union(foundByCategory)
                        .ToList();
                }
            }

            var sorted = orderType switch
            {
                OrderType.Newest => items.OrderByDescending(e => e.ItemId),
                OrderType.Popular => items.OrderByDescending(e => e.Ordered),
                OrderType.LowPrice => items.OrderBy(e => e.Price),
                OrderType.HighPrice => items.OrderByDescending(e => e.Price),
                _ => items.OrderByDescending(e => e.ItemId),
            };

            items = sorted.ToList();

            return PartialView("_ItemList", items);
        }
    }
}
