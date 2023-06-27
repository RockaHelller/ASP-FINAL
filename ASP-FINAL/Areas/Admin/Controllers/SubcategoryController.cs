using ASP_FINAL.Areas.Admin.ViewModels.Category;
using ASP_FINAL.Areas.Admin.ViewModels.Subcategory;
using ASP_FINAL.Data;
using ASP_FINAL.Helpers;
using ASP_FINAL.Models;
using ASP_FINAL.Services;
using ASP_FINAL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SubcategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ISubcategoryService _subcategoryService;

        public SubcategoryController(AppDbContext context, ISubcategoryService subcategoryService)
        {
            _subcategoryService = subcategoryService;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SubcategoryVM> list = new();  // Initialize the list object

            var datas = await _context.SubCategories.Include(m => m.Category).OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new SubcategoryVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    Category = item.Category
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
        public async Task<IActionResult> Create(SubcategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            Subcategory newSubcategory = new()
            {
                Name = request.Name,
                Category = request.Category
            };

            await _context.SubCategories.AddAsync(newSubcategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}
