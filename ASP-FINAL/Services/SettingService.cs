using ASP_FINAL.Data;
using ASP_FINAL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;

namespace ASP_FINAL.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }
        public Dictionary<string, string> GetAll()
        {
            return _context.Settings.AsEnumerable().ToDictionary(m => m.Key, m => m.Value);
        }
    }
}
