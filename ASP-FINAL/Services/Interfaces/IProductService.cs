using ASP_FINAL.Models;

namespace ASP_FINAL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllWithAsync();
        

    }
}
