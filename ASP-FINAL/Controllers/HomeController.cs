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

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllWithAsync();

            HomeVM homeVM = new()
            {
                Products = products.ToList(),
            };

            return View(homeVM);
        }
    }
}