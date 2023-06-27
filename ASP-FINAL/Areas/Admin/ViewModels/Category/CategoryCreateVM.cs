using System.ComponentModel.DataAnnotations;

namespace ASP_FINAL.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }

    }
}
