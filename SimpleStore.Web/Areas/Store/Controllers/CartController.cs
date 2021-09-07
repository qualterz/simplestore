using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Application.Models;
using SimpleStore.Application.Services;
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
        private readonly ICartControllerService cartService;
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public CartController(
            ICartControllerService cartService,
            IOrderService orderService,
            IMapper mapper)
        {
            this.cartService = cartService;
            this.orderService = orderService;
            this.mapper = mapper;
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

        public PartialViewResult CheckoutPartial()
        {
            return PartialView("_Checkout", cartService.GetItems());
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            var cartItems = cartService.GetItems();

            var orderDetails = mapper.Map<List<OrderDetailModel>>(cartItems);

            var order = new OrderModel()
            {
                Details = orderDetails,
            };

            orderService.CreateOrder(order);
            cartService.ClearCart();

            return RedirectToAction("Index", "Home");
        }
    }
}
