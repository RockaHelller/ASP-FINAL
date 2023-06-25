using ASP_FINAL.Areas.Admin.ViewModels.Product;
using ASP_FINAL.Models;
using ASP_FINAL.ViewModels;

namespace ASP_FINAL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllWithAsync();
        //Task<List<Product>> GetPaginatedDatasAsync(int page, int take);
        //Task<Product> GetByIdAsync(int? id);
        //Task<Product> GetByIdWithImagesAsync(int? id);
        //List<ProductVM> GetMappedDatas(List<Product> products);
        //Task<Product> GetWithIncludesAsync(int id);
        //ProductDetailVM GetMappedData(Product product);
        //Task<int> GetCountAsync();
        //Task CreateAsync(ProductCreateVM model);
        //Task DeleteImageByIdAsync(int id);
        //Task EditAsync(int productId, ProductEditVM model);
        //Task DeleteAsync(int id);

    }
}
