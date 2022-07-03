using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Application.Services;
using SimpleStore.Web.Areas.Administration.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SimpleStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class OrdersController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(
            IOrderService orderService,
            IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var orders = orderService.GetOrderList()
                .OrderByDescending(e => e.OrderId)
                .ToList();
            var mapped = mapper.Map<List<OrderViewModel>>(orders);

            return View(mapped);
        }

        public IActionResult Details(int id)
        {
            var order = orderService.GetOrderById(id);
            var mapped = mapper.Map<OrderViewModel>(order);
            return View(mapped.Details);
        }

        public PartialViewResult OrderTable(string search)
        {
            search = search?.Trim().ToLower();
            var orders = orderService.GetOrderList();

            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(
                    e => e.OrderId.ToString() == search)
                    .ToList();
            }

            orders = orders.OrderByDescending(e => e.OrderId).ToList();

            var itemViewModels = mapper.Map<List<OrderViewModel>>(orders);

            return PartialView("_OrderTable", itemViewModels);
        }
    }
}
