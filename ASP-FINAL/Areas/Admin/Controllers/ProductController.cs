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
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAll();
            var subcategories = await _subcategoryService.GetAll();
            var brands = await _context.Brands.ToListAsync();
            var discounts = await _context.Discounts.ToListAsync();

            var tags = await _productService.GetAllWithIncludesAsync();
            List<TagCheckBox> tagCheckBoxes = new();

            foreach (var item in tags)
            {
                tagCheckBoxes.Add(new TagCheckBox { Id = item.Id, Value = item.Name, IsChecked = false });
            }

            var model = new ProductCreateVM
            {
                Categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                Subcategories = subcategories.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }).ToList(),
                Brands = brands.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList(),
                Discounts = discounts.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToList(),
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
                return View();
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







            //if (model.Image == null || model.Image.Count == 0)
            //{
            //    ModelState.AddModelError("Image", "Please select an image file");
            //}
            //else
            //{
            //    foreach (var image in model.Image)
            //    {
            //        if (!IsImageFile(image))
            //        {
            //            ModelState.AddModelError("Image", "Please select only image files");
            //            break;
            //        }
            //        else if (image.Length > 200000) // Adjust the size limit according to your requirements (200 KB = 200,000 bytes)
            //        {
            //            ModelState.AddModelError("Image", "Image size must be max 200 KB");
            //            break;
            //        }
            //    }
            //}

            //if (ModelState.IsValid)
            //{
            //    // Process the image upload
            //    var imageNames = new List<string>();
            //    foreach (var image in model.Image)
            //    {
            //        var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            //        var imagePath = Path.Combine("wwwroot/images/suggest", imageName);
            //        using (var fileStream = new FileStream(imagePath, FileMode.Create))
            //        {
            //            await image.CopyToAsync(fileStream);
            //        }
            //        imageNames.Add(imageName);
            //    }

            //    // Map the view model to the entity
            //    var productImages = imageNames.Select(imageName => new ProductImage { Image = imageName }).ToList();

            //    var product = new Product
            //    {
            //        Name = model.Name,
            //        SKUCode = model.SKUCode,
            //        SalesCount = model.SalesCount,
            //        StockCount = model.StockCount,
            //        Price = model.Price,
            //        Description = model.Description,
            //        SubcategoryId = model.SubcategoryId,
            //        CategoryId = model.CategoryId,
            //        DiscountId = model.DiscountId,
            //        BrandId = model.BrandId,
            //        Images = productImages // Set the collection of product images
            //    };

            //    // Add the product to the database
            //    await _context.Products.AddAsync(product);
            //    await _context.SaveChangesAsync();

            //    // Process the selected tags
            //    if (model.Tags != null && model.Tags.Any())
            //    {
            //        foreach (var tag in model.Tags)
            //        {
            //            if (tag.IsChecked)
            //            {
            //                var productTag = new ProductTag
            //                {
            //                    ProductId = product.Id,
            //                    TagId = tag.Id
            //                };

            //                await _context.ProductTags.AddAsync(productTag);
            //            }
            //        }
            //        await _context.SaveChangesAsync();
            //    }

            //    return RedirectToAction(nameof(Index));
            //}

            //// If the model is not valid, re-populate the dropdown lists and return to the create view with the model
            //var categories = await _categoryService.GetAll();
            //var brands = await _context.Brands.ToListAsync();
            //var discounts = await _context.Discounts.ToListAsync();

            //model.Categories = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList();
            //model.Brands = brands.Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList();
            //model.Discounts = discounts.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }).ToList();

            //return View(model);
        }

        private bool IsImageFile(IFormFile file)
        {
            // Check if the file content type starts with "image/" to ensure it's an image file
            return file.ContentType.StartsWith("image/");
        }


    }
}








