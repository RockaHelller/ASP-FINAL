using Microsoft.AspNetCore.Mvc;

namespace ASP_FINAL.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
