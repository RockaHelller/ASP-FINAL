using ASP_FINAL.Areas.Admin.ViewModels.Category;
using ASP_FINAL.Data;
using ASP_FINAL.Helpers;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Subcategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Subcategory> CreateSubcategoryAsync(Subcategory subcategory)
        {
            await _context.SubCategories.AddAsync(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }

       



    }
}
