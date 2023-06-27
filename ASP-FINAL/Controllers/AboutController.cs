using ASP_FINAL.Data;
using ASP_FINAL.Models;
using ASP_FINAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            List<Member> members = await _context.Members.ToListAsync();
            List<Reason> reasons = await _context.Reasons.ToListAsync();

            AboutVM model = new()
            {
                Members = members,
                Reasons = reasons
            };




            return View(model);
        }
    }
}
