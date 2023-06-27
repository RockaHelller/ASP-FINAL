using ASP_FINAL.Data;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly AppDbContext _context;

        public SubcategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Subcategory>> GetAll()
        {
            return await _context.SubCategories.ToListAsync();
        }

        public async Task<Subcategory> GetByIdAsync(int id)
        {
            return await _context.SubCategories
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
