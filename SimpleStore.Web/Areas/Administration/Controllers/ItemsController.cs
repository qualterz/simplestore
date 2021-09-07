using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Application.Models;
using SimpleStore.Application.Services;
using SimpleStore.Web.Areas.Administration.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStore.Web.Areas.Administration.Controllers
{
    [Authorize]
    [Area("Administration")]
    public class ItemsController : Controller
    {
        private readonly IItemService itemService;
        private readonly ICharacteristicService characteristicService;
        private readonly IMapper mapper;

        public ItemsController(
            IItemService itemService,
            ICharacteristicService characteristicService,
            IMapper mapper)
        {
            this.itemService = itemService;
            this.characteristicService = characteristicService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var items = itemService.GetItemList();
            var itemViewModels = mapper.Map<List<ItemViewModel>>(items);

            return View(itemViewModels);
        }

        public IActionResult Edit(int id)
        {
            var itemViewModel = new ItemViewModel();

            if (id != default)
            {
                var item = itemService.GetItemById(id);
                itemViewModel = mapper.Map<ItemViewModel>(item);
                itemViewModel.Characteristics.ForEach(e => e.ItemId = item.ItemId);
            }

            return View(itemViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ItemViewModel itemViewModel)
        {
            if (!ModelState.IsValid)
                return View("Edit", itemViewModel);

            var item = mapper.Map<ItemModel>(itemViewModel);

            if (itemViewModel.ItemId == default)
            {
                item = itemService.AddItem(item);
                return RedirectToAction("Edit", new { id = item.ItemId });
            }
            else
            {
                itemService.UpdateItem(item);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = itemService.GetItemById(id);
            itemService.DeleteItem(item);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AssignCharacteristic(CharacteristicViewModel characteristicViewModel)
        {
            var itemId = characteristicViewModel.ItemId;
            var characteristic = mapper.Map<CharacteristicModel>(characteristicViewModel);

            characteristicService.AssignCharacteristic(characteristic, itemId);

            return RedirectToAction("Edit", new { id = itemId });
        }

        [HttpPost]
        public IActionResult UnassignCharacteristic(CharacteristicViewModel characteristicViewModel)
        {
            var characteristicId = characteristicViewModel.CharacteristicId;
            var itemId = characteristicViewModel.ItemId;

            characteristicService.UnassignCharacteristic(characteristicId, itemId);

            return RedirectToAction("Edit", new { id = itemId });
        }

        public PartialViewResult ItemTable(string search)
        {
            search = search?.Trim().ToLower();
            var items = itemService.GetItemList();

            if (!string.IsNullOrEmpty(search))
            {
                items = items.Where(
                    e => e.ItemId.ToString() == search ||
                    e.Name.ToLower().Contains(search)).ToList();
            }

            var itemViewModels = mapper.Map<List<ItemViewModel>>(items);

            return PartialView("_ItemTable", itemViewModels);
        }
    }
}
