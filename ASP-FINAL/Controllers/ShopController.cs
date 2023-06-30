using ASP_FINAL.Data;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using ASP_FINAL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP_FINAL.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly AppDbContext _context;



        public ShopController(IProductService productService,
                                     ISettingService settingService,
                                     ICategoryService categoryService,
                                     AppDbContext context,
                                     ISubcategoryService subcategoryService)
        {
            _productService = productService;
            _settingService = settingService;
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _context = context;

        }
        public async Task<IActionResult> Index(int id)
        {

            IEnumerable<Product> product = await _productService.GetAllWithIncludesAsync();

            LayoutVM model = new LayoutVM
            {
                Products = product.ToList(),
            };

            return View(model);
        }
    }
}
