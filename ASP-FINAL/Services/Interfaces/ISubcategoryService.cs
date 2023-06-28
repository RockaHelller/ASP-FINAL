﻿using ASP_FINAL.Areas.Admin.ViewModels.Subcategory;
using ASP_FINAL.Models;

namespace ASP_FINAL.Services.Interfaces
{
    public interface ISubcategoryService
    {
        Task<List<Subcategory>> GetAll();
        Task<Subcategory> GetByIdAsync(int id);
        Task AddAsync(SubcategoryCreateVM model);

    }
}
