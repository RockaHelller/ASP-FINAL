using ASP_FINAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_FINAL.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILayoutService _layoutService;

        public ProfileController(ILayoutService layoutService)
        {
            _layoutService = layoutService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var datas = await _layoutService.GetAllDatas();
            return await Task.FromResult(View(datas));
        }
    }
}
