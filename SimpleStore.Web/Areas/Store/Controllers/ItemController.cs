using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Application.Services;
using SimpleStore.Web.Areas.Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleStore.Web.Areas.Store.Controllers
{
    [Area("Store")]
    public class ItemController : Controller
    {
        private readonly IItemService itemService;
        private readonly IMapper mapper;
        public ItemController(
            IItemService itemService,
            IMapper mapper)
        {
            this.itemService = itemService;
            this.mapper = mapper;
        }

        public IActionResult Index(int id)
        {
            var item = itemService.GetItemById(id);
            var itemViewModel = mapper.Map<ItemViewModel>(item);

            return View(itemViewModel);
        }
    }
}
