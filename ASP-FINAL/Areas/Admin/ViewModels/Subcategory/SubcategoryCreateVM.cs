using System.ComponentModel.DataAnnotations;

namespace ASP_FINAL.Areas.Admin.ViewModels.Subcategory
{
    public class SubcategoryCreateVM
    {
        [Required(ErrorMessage = "Please enter the subcategory name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        public List<Models.Category> Categories { get; set; }
    }
}
