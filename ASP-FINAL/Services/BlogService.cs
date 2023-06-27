using ASP_FINAL.Areas.Admin.ViewModels;
using ASP_FINAL.Areas.Admin.ViewModels.Blog;
using ASP_FINAL.Data;
using ASP_FINAL.Helpers;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP_FINAL.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        public async Task<int> GetCountAsync() => await _context.Blogs.CountAsync();

        public async Task CreateAsync(List<IFormFile> images, string title, string description)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/blog");

                Blog blog = new()
                {
                    Image = fileName,
                    Title = title,
                    Description = description,
    

                };

                await _context.Blogs.AddAsync(blog);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Blog blog = await GetByIdAsync(id);

            _context.Blogs.Remove(blog);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", blog.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int id, BlogEditVM model)
        {

            var blog = await GetWithIncludesAsync(id);

            if (blog == null)
            {
                // Handle the case where the slider with the given ID doesn't exist
                // or return an appropriate response
            }

            if (model.NewImage != null)
            {
                foreach (var item in model.NewImage)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    await item.SaveFileAsync(fileName, _env.WebRootPath, "images/blog");
                    blog.Image = fileName;
                    blog.Image = fileName;

                }
            }

            blog.Title = model.NewTitle;
            blog.Description = model.NewDesc;

            await _context.SaveChangesAsync();

        }


        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        //public async Task<List<Blog>> GetAllByStatusAsync()
        //{
        //    return await _context.Sliders.ToListAsync();
        //}

        public async Task<List<BlogVM>> GetAllMappedDatasAsync()
        {
            List<BlogVM> bloglist = new();

            List<Blog> blogs = await GetAllAsync();

            foreach (Blog blog in blogs)
            {
                BlogVM model = new()
                {
                    Id = blog.Id,
                    Image = blog.Image,
                    Title = blog.Title,
                };

                bloglist.Add(model);
            }

            return bloglist;
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
        }

        public BlogDetailVM GetMappedData(Blog blog)
        {
            BlogDetailVM model = new()
            {
                Image = blog.Image,
                Title = blog.Title,
                Description = blog.Description,
                CreateDate = blog.CreateDate.ToString("dddd, dd MMMM yyyy"),

            };

            return model;
        }


        public async Task<Blog> GetWithIncludesAsync(int id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
        }


    }
}
