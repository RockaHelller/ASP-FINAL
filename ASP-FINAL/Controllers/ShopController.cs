using Microsoft.AspNetCore.Mvc;

namespace ASP_FINAL.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
