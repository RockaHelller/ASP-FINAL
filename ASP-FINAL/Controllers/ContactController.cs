using ASP_FINAL.Data;
using ASP_FINAL.Models;
using ASP_FINAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Branch> branches = await _context.Branches.ToListAsync();
            List<Location> locations = await _context.Locations.ToListAsync();


            ContactVM model = new()
            {
                Branch = branches,
                Location = locations,
            };

            return View(model);
        }
    }
}
