using ASP_FINAL.Areas.Admin.ViewModels.Blog;
using ASP_FINAL.Models;

namespace ASP_FINAL.Services.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task<List<BlogVM>> GetAllMappedDatasAsync();
        BlogDetailVM GetMappedData(Blog blog);
        Task CreateAsync(List<IFormFile> images, string title, string description);
        Task DeleteAsync(int id);
        Task EditAsync(int id, BlogEditVM model);
        //Task<List<Blog>> GetAllByStatusAsync();
        Task<int> GetCountAsync();
        Task<Blog> GetWithIncludesAsync(int id);
    }
}
