using Microsoft.AspNetCore.Mvc;
using SimpleStore.Application.Services;

namespace SimpleStore.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class HomeController : Controller
    {
        private readonly IItemService itemService;

        public HomeController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Items");
        }
    }
}
