using Microsoft.AspNetCore.Mvc;
using SimpleStore.Web.Areas.Store.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleStore.Web.Areas.Store.Controllers
{
    [Area("Store")]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public IActionResult Index()
        {
            var cartItems = cartService.GetItems();

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult Add(int id)
        {
            cartService.Add(id);

            return Ok();
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            cartService.Remove(id);

            return Ok();
        }

        [HttpPost]
        public IActionResult Update(int id, int quantity)
        {
            cartService.Update(id, quantity);

            return Ok();
        }
    }
}
