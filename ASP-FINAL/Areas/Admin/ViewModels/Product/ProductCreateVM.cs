using ASP_FINAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ASP_FINAL.Areas.Admin.ViewModels.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public int SKUCode { get; set; }
        public int SalesCount { get; set; }
        public int StockCount { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int SubcategoryId { get; set; }
        public int CategoryId { get; set; }
        public int DiscountId { get; set; }
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Please Add Image")]
        public List<IFormFile> Image { get; set; }
        
        //public List<SelectListItem> Categories { get; set; }
        //public List<SelectListItem> Brands { get; set; }
        //public List<SelectListItem> Discounts { get; set; }
        //public List<SelectListItem> Subcategories { get; set; }
        public List<TagCheckBox> Tags { get; set; }

    }
}
