using ASP_FINAL.ViewModels;

namespace ASP_FINAL.Services.Interfaces
{
    public interface ILayoutService
    {
        Task<LayoutVM> GetAllDatas();

    }
}
