using ASP_FINAL.Areas.Admin.ViewModels;
using ASP_FINAL.Data;
using ASP_FINAL.Helpers;
using ASP_FINAL.Models;
using ASP_FINAL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ASP_FINAL.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;

        }

        public async Task<int> GetCountAsync() => await _context.Sliders.Where(m => m.Status).CountAsync();

        public async Task CreateAsync(List<IFormFile> images, string title, string description)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "images/home/index");

                Slider slider = new()
                {
                    Image = fileName,
                    Title = title,
                    Descriptiom = description,
                };

                await _context.Sliders.AddAsync(slider);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Slider slider = await GetByIdAsync(id);

            _context.Sliders.Remove(slider);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", slider.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int id, SliderEditVM model)
        {

            var slider = await GetWithIncludesAsync(id);

            if (slider == null)
            {
                // Handle the case where the slider with the given ID doesn't exist
                // or return an appropriate response
            }

            if (model.NewImage != null)
            {
                foreach (var item in model.NewImage)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    await item.SaveFileAsync(fileName, _env.WebRootPath, "images/home/index");
                    slider.Image = fileName;
                }
            }

            slider.Title = model.NewTitle;
            slider.Descriptiom = model.NewDesc;

            await _context.SaveChangesAsync();

        }


        public async Task<List<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<List<Slider>> GetAllByStatusAsync()
        {
            return await _context.Sliders.Where(m => m.Status).ToListAsync();
        }

        public async Task<List<SliderVM>> GetAllMappedDatasAsync()
        {
            List<SliderVM> sliderList = new();

            List<Slider> sliders = await GetAllAsync();

            foreach (Slider slider in sliders)
            {
                SliderVM model = new()
                {
                    Id = slider.Id,
                    Image = slider.Image,
                    Status = slider.Status,
                    CreateDate = slider.CreateDate.ToString("dddd, dd MMMM yyyy"),
                };

                sliderList.Add(model);
            }

            return sliderList;
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
        }

        public SliderDetailVM GetMappedData(Slider slider)
        {
            SliderDetailVM model = new()
            {
                Image = slider.Image,
                Status = slider.Status,
                CreateDate = slider.CreateDate.ToString("dddd, dd MMMM yyyy"),

            };

            return model;
        }

        public async Task<bool> ChangeStatusAsync(Slider slider)
        {
            if (slider.Status && await GetCountAsync() != 1)
            {
                slider.Status = false;
            }
            else
            {
                slider.Status = true;
            }

            await _context.SaveChangesAsync();

            return slider.Status;
        }


        public async Task<Slider> GetWithIncludesAsync(int id)
        {
            return await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}