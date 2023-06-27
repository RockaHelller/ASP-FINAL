using ASP_FINAL.Areas.Admin.ViewModels.Category;
using ASP_FINAL.Areas.Admin.ViewModels.Product;
using ASP_FINAL.Models;

namespace ASP_FINAL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category> GetByIdAsync(int id);
        public Task<Subcategory> CreateSubcategoryAsync(Subcategory subcategory);


    }
}
