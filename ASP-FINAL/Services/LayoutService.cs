using ASP_FINAL.Data;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using ASP_FINAL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASP_FINAL.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductService _productService;


        public LayoutService(AppDbContext context,
                             IHttpContextAccessor httpContextAccessor,
                             UserManager<AppUser> userManager,
                             IProductService productService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _productService = productService;

        }

        public async Task<LayoutVM> GetAllDatas()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var datas = _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
            var categories = _context.Categories.Include(c => c.Subcategories).ToList();
            var products = await _productService.GetAllWithIncludesAsync();


            var layoutVM = new LayoutVM
            {
                SettingDatas = datas,
                UserFullName = user?.FullName,
                UserEmail = user?.Email,
                Categories = categories,
                Products = products.ToList(),

            };

            return layoutVM;
        }

    }
}
