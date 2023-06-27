using ASP_FINAL.Models;

namespace ASP_FINAL.Areas.Admin.ViewModels.Category
{
    public class CategoryDetailVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string CreateDate { get; set; }
        public Subcategory Subcategory { get; set; }

    }
}
