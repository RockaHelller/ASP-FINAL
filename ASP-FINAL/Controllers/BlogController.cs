using ASP_FINAL.Areas.Admin.ViewModels.Product;
using ASP_FINAL.Data;
using ASP_FINAL.Helpers;
using ASP_FINAL.Models;
using ASP_FINAL.Services;
using ASP_FINAL.Services.Interfaces;
using ASP_FINAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page)
        {
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            int pageSize = 5; // Number of blogs to display per page
            int totalItems = blogs.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var pagedBlogs = blogs
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new List<BlogVM>();
            foreach (var blog in pagedBlogs)
            {
                model.Add(new BlogVM
                {
                    Blogs = new List<Blog> { blog }
                });
            }

            var paginatedModel = new Paginate<BlogVM>(model, page, totalPages);

            return View(paginatedModel);
        }



    }
}

