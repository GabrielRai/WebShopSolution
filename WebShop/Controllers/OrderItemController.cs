using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    public class OrderItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
