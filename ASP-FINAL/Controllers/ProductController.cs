using ASP_FINAL.Data;
using ASP_FINAL.Models;
using ASP_FINAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            Product product = await _context.Products.Include(m => m.Discount).Include(m => m.Images).Include(m => m.Category).Include(m=>m.Brand).FirstOrDefaultAsync(x => x.Id == id);

            if (product is null) return NotFound();

            ProductVM model = new()
            {
                Id = product.Id,
                Brand = product.Brand,
                Category = product.Category,
                Subcategory = product.SubCategory,
                Name = product.Name,
                Discount = product.Discount,
                Image = product.Images,
                Price = product.Price,
                StockCount = product.StockCount,
            };

            return View(model);
        }




    }
}
