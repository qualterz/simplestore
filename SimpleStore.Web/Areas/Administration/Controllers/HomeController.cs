using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Application.Services;

namespace SimpleStore.Web.Areas.Administration.Controllers
{
    [Authorize]
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
            if (HttpContext.User is null)
                return RedirectToAction("Index", "Login", new { area = "Account" } );

            return RedirectToAction("Index", "Items");
        }
    }
}
