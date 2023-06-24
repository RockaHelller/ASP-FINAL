using ASP_FINAL.Data;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>>  GetAllWithAsync()
        {
            return await _context.Products.Include(m => m.ProductTags).
                ThenInclude(m => m.Tag).Include(m => m.SubCategory).
                Include(m => m.Category).Include(m => m.Discount).
                Include(m => m.Brand).Include(m => m.Rating).
                Include(m => m.Reviews).ThenInclude(m => m.AppUser).
                Include(m => m.Images).ToListAsync();
        }

  
    }
}
