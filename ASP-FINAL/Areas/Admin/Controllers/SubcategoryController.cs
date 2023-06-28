﻿using ASP_FINAL.Areas.Admin.ViewModels.Category;
using ASP_FINAL.Areas.Admin.ViewModels.Subcategory;
using ASP_FINAL.Data;
using ASP_FINAL.Helpers;
using ASP_FINAL.Models;
using ASP_FINAL.Services;
using ASP_FINAL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly ICategoryService _categoryService;


        public SubcategoryController(AppDbContext context, ISubcategoryService subcategoryService, ICategoryService categoryService)
        {
            _subcategoryService = subcategoryService;
            _context = context;
            _categoryService = categoryService;
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
        public async Task<IActionResult> Create()
        {
            var model = new SubcategoryCreateVM();
            model.Categories = await _categoryService.GetAll();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubcategoryCreateVM request)
        {
            await GetAllSelectOptions();

            await _subcategoryService.AddAsync(request);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Subcategory subcategory = await _context.SubCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (subcategory == null)
            {
                // Handle the case when the subcategory does not exist
                return NotFound();
            }

            _context.SubCategories.Remove(subcategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }








    }




}
