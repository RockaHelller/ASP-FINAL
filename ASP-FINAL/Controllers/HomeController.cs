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
        private readonly ICategoryService _categoryService;


        public HomeController(IProductService productService, AppDbContext context, ICategoryService categoryService)
        {
            _productService = productService;
            _context = context;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithIncludesAsync();
            var settings = await _context.Settings.ToListAsync();
            var blogs = await _context.Blogs.OrderByDescending(m=>m.Id).ToListAsync();
            var category = await _categoryService.GetAll();

            HomeVM model = new()
            {
                Products = products.ToList(),
                SettingDatas = settings.AsEnumerable().ToDictionary(m=>m.Key, m=>m.Value),
                Blog = new BlogVM { Blogs = blogs},
                Categories = category
            };

            return View(model);
        }
    }
}