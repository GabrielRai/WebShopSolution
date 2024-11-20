using Microsoft.AspNetCore.Mvc;

namespace WebShop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
