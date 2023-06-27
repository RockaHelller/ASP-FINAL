using ASP_FINAL.Areas.Admin.ViewModels.Product;
using ASP_FINAL.Data;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public Task AddAsync(ProductCreateVM model)
        {
            throw new NotImplementedException();
        }

        //public async Task DeleteAsync(int id)
        //{
        //    Product product = await GetByIdAsync(id);

        //    _context.Products.Remove(product);

        //    await _context.SaveChangesAsync();

        //    string path = Path.Combine(_env.WebRootPath, "wwwroot/images/suggest", product.Images.FirstOrDefault().Image);

        //    if (File.Exists(path))
        //    {
        //        File.Delete(path);
        //    }
        //}

        public Task EditAsync(int productId, ProductEditVM model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllWithIncludesAsync()
        {
            return await _context.Products.Include(m => m.ProductTags).
                ThenInclude(m => m.Tag).Include(m => m.SubCategory).
                Include(m => m.Category).Include(m => m.Discount).
                Include(m => m.Brand).Include(m => m.Rating).
                Include(m => m.Reviews).ThenInclude(m => m.AppUser).
                Include(m => m.Images).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int? id) => await _context.Products.Include(m => m.Images).
                                                                                            Include(m => m.ProductTags).
                                                                                            Include(m => m.Discount).
                                                                                            Include(m => m.SubCategory).
                                                                                            Include(m => m.Category).
                                                                                            Include(m => m.Brand).
                                                                                            Include(m => m.Rating).
                                                                                            Include(m => m.Reviews).
                                                                                            Include(m => m.ProductTags).ThenInclude(m => m.Tag).
                                                                                            FirstOrDefaultAsync(m => m.Id == id);

        public async Task<Product> GetByIdWithAllIncludes(int id)
        {
            return await _context.Products
                .Include(p => p.SubCategory)
                .Include(p => p.Category)
                .Include(p => p.Discount)
                .Include(p => p.Brand)
                .Include(p => p.Rating)
                .Include(p => p.Reviews)
                .Include(p => p.Images)
                .Include(p => p.ProductTags) // Include ProductTags
                .ThenInclude(p => p.Tag)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public ProductDetailVM GetMappedData(Product product)
        {
            ProductDetailVM productDetail = new ProductDetailVM
            {
                Name = product.Name,
                Image = product.Images,
                SKU = product.SKUCode,
                SalesCount = product.SalesCount,
                StockCount = product.StockCount,
                Price = product.Price.ToString("0.#####") + " ₼",
                Description = product.Description,
                Subcategory = product.SubCategory,
                Category = product.Category?.Name,
                Brand = product.Brand?.Name,
                Discount = product.Discount,
                Rating = product.Rating?.RatingCount ?? 0,
                Reviews = product.Reviews,
                ProductWishlists = product.ProductWishLists,
                ProductTags = product.ProductTags,
                ProductBaskets = product.ProductBaskets,
                CreateDate = product.CreateDate.ToString("dddd, dd MMMM yyyy")
            };
            return productDetail;
        }
    }
}
