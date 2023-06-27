using System.ComponentModel.DataAnnotations;

namespace ASP_FINAL.Areas.Admin.ViewModels.Subcategory
{
    public class SubcategoryCreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public ASP_FINAL.Models.Category Category { get; set; }
    }
}
