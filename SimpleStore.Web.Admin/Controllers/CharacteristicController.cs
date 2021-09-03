using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Entities;
using SimpleStore.Services;
using SimpleStore.Web.ViewModels;

namespace SimpleStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class CharacteristicController : Controller
    {
        private readonly IItemService itemService;
        private readonly IMapper mapper;

        public CharacteristicController(
            IItemService itemService,
            IMapper mapper)
        {
            this.itemService = itemService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(int itemId)
        {
            var item = itemService.GetItem(itemId);
            var itemViewModel = mapper.Map<ItemViewModel>(item);

            return View(itemViewModel);
        }

        [HttpGet]
        public IActionResult Assign(int itemId)
        {
            var characteristicViewModel = new CharacteristicViewModel()
            {
                ItemId = itemId,
            };

            return View(characteristicViewModel);
        }

        [HttpPost]
        public IActionResult Assign(CharacteristicViewModel characteristicViewModel)
        {
            if (!ModelState.IsValid)
                return View(characteristicViewModel);

            var characteristic = mapper.Map<Characteristic>(characteristicViewModel);
            characteristic = itemService.CreateCharacteristic(characteristic);

            itemService.AssignCharacteristic(characteristic.Id, characteristicViewModel.ItemId);

            return RedirectToAction("Edit", "Item", new { Id = characteristicViewModel.ItemId });
        }

        [HttpPost]
        public IActionResult Unassign(int characteristicId, int itemId)
        {
            itemService.UnassignCharacteristic(characteristicId, itemId);

            return RedirectToAction("Edit", "Item", new { Id = itemId });
        }
    }
}
