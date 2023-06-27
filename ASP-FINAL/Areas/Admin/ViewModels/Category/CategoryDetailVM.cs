using ASP_FINAL.Models;

namespace ASP_FINAL.Areas.Admin.ViewModels.Category
{
    public class CategoryDetailVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string CreateDate { get; set; }
        public ASP_FINAL.Models.Subcategory Subcategory { get; set; }

    }
}
