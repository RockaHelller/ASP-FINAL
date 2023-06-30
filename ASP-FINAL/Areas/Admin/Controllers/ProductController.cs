using ASP_FINAL.Areas.Admin.ViewModels.Category;
using ASP_FINAL.Areas.Admin.ViewModels.Product;
using ASP_FINAL.Data;
using ASP_FINAL.Helpers;
using ASP_FINAL.Models;
using ASP_FINAL.Services;
using ASP_FINAL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Cryptography.X509Certificates;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace ASP_FINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly AppDbContext _context;



        public ProductController(IProductService productService,
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

        private async Task GetAllSelectOptions()
        {
            ViewBag.categories = await GetCategories();
        }
        private async Task<SelectList> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryService.GetAll();
            return new SelectList(categories, "Id", "Name");
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5; // Number of products to display per page

            var products = await _productService.GetAllWithIncludesAsync();

            int totalItems = products.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

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
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAll();
            var subcategories = await _subcategoryService.GetAll();
            var brands = await _context.Brands.ToListAsync();
            var discounts = await _context.Discounts.ToListAsync();

            var tags = await _context.Tags.ToListAsync();
            List<TagCheckBox> tagCheckBoxes = new();

            foreach (var item in tags)
            {
                tagCheckBoxes.Add(new TagCheckBox { Id = item.Id, Value = item.Name, IsChecked = false });
            }

            ViewBag.categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            ViewBag.subcategories = subcategories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            ViewBag.brands = brands.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            ViewBag.discounts = discounts.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();


            var model = new ProductCreateVM
            {
                Tags = tagCheckBoxes
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {

            await GetAllSelectOptions();
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAll();
                var subcategories = await _subcategoryService.GetAll();
                var brands = await _context.Brands.ToListAsync();
                var discounts = await _context.Discounts.ToListAsync();

                ViewBag.categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
                ViewBag.subcategories = subcategories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
                ViewBag.brands = brands.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
                ViewBag.discounts = discounts.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
                return View(model);
            }

            foreach (var item in model.Image)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Please select only image file");
                    return View();
                }

                if (item.CheckFileSize(2000))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 KB");
                    return View();
                }
            }

            await _productService.CreateAsync(model);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var existProduct = await _productService.GetByIdAsync(id);
            if (existProduct is null)
                return NotFound();

            var tags = await _context.Tags.ToListAsync();
            List<TagCheckBox> tagCheckBoxes = new();

            foreach (var item in tags)
            {
                tagCheckBoxes.Add(new TagCheckBox { Id = item.Id, Value = item.Name, IsChecked = IsChekedTag(id, item) });
            }

            var model = new ProductEditVM
            {
                Id = existProduct.Id,
                Name = existProduct.Name,
                SKUCode = existProduct.SKUCode,
                SalesCount = existProduct.SalesCount,
                StockCount = existProduct.StockCount,
                Price = existProduct.Price,
                Description = existProduct.Description,
                SubcategoryId = existProduct.SubcategoryId,
                CategoryId = existProduct.CategoryId,
                DiscountId = existProduct.DiscountId,
                BrandId = existProduct.BrandId,
                Tags = tagCheckBoxes,
            };

            var categories = await _context.Categories.ToListAsync();
            ViewBag.categories = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var subcategories = await _context.SubCategories.ToListAsync();
            ViewBag.subcategories = subcategories.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            var brands = await _context.Brands.ToListAsync();
            ViewBag.brands = brands.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            }).ToList();

            var discounts = await _context.Discounts.ToListAsync();
            ViewBag.discounts = discounts.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductEditVM model)
        {
            if (id == null)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories.ToListAsync();
                ViewBag.categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                var subcategories = await _context.SubCategories.ToListAsync();
                ViewBag.subcategories = subcategories.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();

                var brands = await _context.Brands.ToListAsync();
                ViewBag.brands = brands.Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList();

                var discounts = await _context.Discounts.ToListAsync();
                ViewBag.discounts = discounts.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList();

                return View(model);
            }

            await _productService.EditAsync(model);

            return RedirectToAction(nameof(Index));



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }










        private bool IsImageFile(IFormFile file)
        {
            return file.ContentType.StartsWith("image/");
        }

        private bool IsChekedTag(int? id, Tag item)
        {
            if (item.ProductTags != null)
            {
                foreach (var tag in item.ProductTags)
                {
                    if (tag.ProductId == id)
                    {
                        return true;

                    }
                    else
                    {
                        return false;
                    }

                }
            }

            return false;

        }
    }
}








