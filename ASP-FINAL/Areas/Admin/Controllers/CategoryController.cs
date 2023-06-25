using ASP_FINAL.Areas.Admin.ViewModels.Category;
using ASP_FINAL.Data;
using ASP_FINAL.Helpers;
using ASP_FINAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            List<CategoryVM> list = new();

            var datas = await _context.Categories.OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new CategoryVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                });
            }

            return View(list);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
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

            string imageName = null;

            if (request.Images != null && request.Images.Count > 0)
            {
                var image = request.Images[0];

                if (image != null && image.Length > 0)
                {
                    // Generate a unique image name or use a naming convention that suits your requirements
                    imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                    // Save the image to a specified location or a database, depending on your implementation
                    var imagePath = Path.Combine("wwwroot/images/suggest", imageName);
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }
            }

            Category newCategory = new()
            {
                Name = request.Name,
                Image = imageName
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Create(CategoryCreateVM request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    Category newCategory = new()
        //    {
        //        Name = request.Name,
        //        Image = request.Images
        //    };

        //    await _context.Categories.AddAsync(newCategory);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var existCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (existCategory is null) return NotFound();

            CategoryEditVM model = new()
            {
                Id = existCategory.Id,
                Name = existCategory.Name,
                Image = existCategory.Image,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (id is null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
            if (existCategory is null) return NotFound();

            if (existCategory.Name.Trim() == request.Name.Trim())
            {
                return RedirectToAction(nameof(Index));
            }

            Category category = new()
            {
                Id = request.Id,
                Name = request.Name,
                Image = request.Image,
            };

            _context.Update(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var existCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

        //    _context.Remove(existCategory);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var existCategory = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);

            existCategory.SoftDelete = true;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
