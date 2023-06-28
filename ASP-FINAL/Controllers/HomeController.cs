using ASP_FINAL.Data;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using ASP_FINAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP_FINAL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;
        private readonly ILayoutService _layoutService;

        public HomeController(IProductService productService, AppDbContext context)
        {
            _productService = productService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithIncludesAsync();
            var settings = await _context.Settings.ToListAsync();
            var blogs = await _context.Blogs.OrderByDescending(m=>m.Id).ToListAsync();

            HomeVM model = new()
            {
                Products = products.ToList(),
                SettingDatas = settings.AsEnumerable().ToDictionary(m=>m.Key, m=>m.Value),
                Blog = new BlogVM { Blogs = blogs},

            };

            return View(model);
        }
    }
}