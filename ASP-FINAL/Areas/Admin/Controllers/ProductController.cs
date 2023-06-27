using ASP_FINAL.Areas.Admin.ViewModels.Product;
using ASP_FINAL.Helpers;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_FINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
        public class ProductController : Controller
        {
            private readonly IProductService _productService;
            private readonly ISettingService _settingService;
            private readonly ICategoryService _categoryService;


            public ProductController(IProductService productService,
                                     ISettingService settingService,
                                     ICategoryService categoryService)
            {
                _productService = productService;
                _settingService = settingService;
                _categoryService = categoryService;
            }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10; // Number of products to display per page

            var products = await _productService.GetAllWithIncludesAsync();

            // Calculate total number of pages
            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Retrieve the products for the current page
            var pagedProducts = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            List<ProductVM> model = new List<ProductVM>();
            foreach (var item in pagedProducts)
            {
                model.Add(new ProductVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Brand = item.Brand?.Name,
                    Image = item.Images?.FirstOrDefault()?.Image,
                    StockCount = item.StockCount,
                    Discount = item.Discount?.Name,
                    CategoryName = item.Category?.Name,
                    Description = item.Description,
                    Price = item.Price.ToString("0.#####"),
                    
                });
            }

            // Create the Paginate object
            var paginatedModel = new Paginate<ProductVM>(model, page, totalPages);

            return View(paginatedModel);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
                return BadRequest();

            var product = await _productService.GetByIdWithAllIncludes(id.Value);

            if (product is null)
                return NotFound();

            var productDetail = _productService.GetMappedData(product);

            return View(productDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {


            return View();
        }
    }
}






    

