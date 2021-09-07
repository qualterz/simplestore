using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Web.Areas.Store.Services;
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
        private readonly IItemControllerService itemService;
        public ItemController(IItemControllerService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult Index(int id)
        {
            var item = itemService.GetItemById(id);
            return View(item);
        }
    }
}
