using ASP_FINAL.Areas.Admin.ViewModels;
using ASP_FINAL.Areas.Admin.ViewModels.Blog;
using ASP_FINAL.Helpers;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP_FINAL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class BlogController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IBlogService _blogservice;

        public BlogController(IWebHostEnvironment env, IBlogService blogService)
        {
            _env = env;
            _blogservice = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _blogservice.GetAllMappedDatasAsync());

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Blog dbBlog = await _blogservice.GetByIdAsync((int)id);

            if (dbBlog is null) return NotFound();

            return View(_blogservice.GetMappedData(dbBlog));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Please select only image file");
                    return View();
                }

                if (item.CheckFileSize(2000))
                {
                    ModelState.AddModelError("Image", "Image size must be max 2000 KB");
                    return View();
                }
            }

            await _blogservice.CreateAsync(request.Images, request.Title, request.Description);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogservice.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Blog dbBlog = await _blogservice.GetByIdAsync((int)id);

            if (dbBlog is null) return NotFound();

            return View(new BlogEditVM { Image = dbBlog.Image, NewTitle = dbBlog.Title, NewDesc = dbBlog.Description });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogEditVM request)
        {
            if (id is null) return BadRequest();

            Blog dbBlog = await _blogservice.GetWithIncludesAsync((int)id);
            if (dbBlog is null) return NotFound();

            //if (request.NewImage is null) return RedirectToAction(nameof(Index));

            if (request.NewImage != null)
            {
                foreach (var item in request.NewImage)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Image", "Please select only image file");
                        request.Image = dbBlog.Image;
                        return View(request);
                    }

                    if (item.CheckFileSize(20000))
                    {
                        ModelState.AddModelError("Image", "Image size must be max 20 MB");
                        request.Image = dbBlog.Image;
                        return View(request);
                    }
                }
            }

            await _blogservice.EditAsync((int)id, request);

            return RedirectToAction(nameof(Index));
        }


    }

}
