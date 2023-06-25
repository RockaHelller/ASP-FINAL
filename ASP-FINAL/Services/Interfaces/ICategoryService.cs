using ASP_FINAL.Models;

namespace ASP_FINAL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAll();
    }
}
