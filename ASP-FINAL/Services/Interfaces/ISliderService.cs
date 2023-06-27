using ASP_FINAL.Areas.Admin.ViewModels;
using ASP_FINAL.Models;
using System.Security;

namespace ASP_FINAL.Services.Interfaces
{
    public interface ISliderService
    {
        Task<List<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task<List<SliderVM>> GetAllMappedDatasAsync();
        SliderDetailVM GetMappedData(Slider slider);
        Task CreateAsync(List<IFormFile> images, string title, string description);
        Task DeleteAsync(int id);
        Task EditAsync(int id, SliderEditVM model);
        Task<List<Slider>> GetAllByStatusAsync();
        Task<int> GetCountAsync();
        Task<bool> ChangeStatusAsync(Slider slider);
        Task<Slider> GetWithIncludesAsync(int id);


    }
}
