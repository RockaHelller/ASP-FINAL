using Microsoft.AspNetCore.Mvc;

namespace ASP_FINAL.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
