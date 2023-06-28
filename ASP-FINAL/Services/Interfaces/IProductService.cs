using ASP_FINAL.Areas.Admin.ViewModels.Product;
using ASP_FINAL.Models;

namespace ASP_FINAL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllWithIncludesAsync();
        Task<Product> GetByIdAsync(int? id);
        Task<Product> GetByIdWithAllIncludes(int id);
        ProductDetailVM GetMappedData(Product product);
        Task EditAsync(int productId, ProductEditVM model);
        Task AddAsync(ProductCreateVM model);
        Task CreateAsync(ProductCreateVM model);


        //Task DeleteAsync(int id);






        //Task<IEnumerable<Product>> GetAllAsync();
        //Task<List<Product>> GetPaginatedDatasAsync(int page, int take);
        //Task<Product> GetByIdWithImagesAsync(int? id);
        //List<ProductVM> GetMappedDatas(List<Product> products);
        //Task<Product> GetWithIncludesAsync(int id);
        //Task<int> GetCountAsync();
        //Task CreateAsync(ProductCreateVM model);
        //Task DeleteImageByIdAsync(int id);

    }
}
