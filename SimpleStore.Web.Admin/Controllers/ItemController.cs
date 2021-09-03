using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Entities;
using SimpleStore.Services;
using SimpleStore.Web.ViewModels;

namespace SimpleStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ItemController : Controller
    {
        private readonly IMapper mapper;
        private readonly IItemService itemService;

        public ItemController(
            IMapper mapper,
            IItemService itemService)
        {
            this.mapper = mapper;
            this.itemService = itemService;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var itemViewModel = new ItemViewModel();

            if (id != default)
            {
                var item = itemService.GetItem(id);
                itemViewModel = mapper.Map<ItemViewModel>(item);
            }

            return View(itemViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ItemViewModel itemViewModel)
        {
            if (!ModelState.IsValid)
                return View("Edit", itemViewModel);

            var item = mapper.Map<Item>(itemViewModel);

            if (itemViewModel.Id == default)
            {
                itemService.CreateItem(item);
                return RedirectToAction("Edit", "Item", new { itemViewModel.Id });
            }

            itemService.UpdateItem(item);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            itemService.DeleteItem(id);

            return RedirectToAction("Index", "Home");
        }
    }
}
