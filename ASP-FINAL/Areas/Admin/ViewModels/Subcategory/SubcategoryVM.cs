namespace ASP_FINAL.Areas.Admin.ViewModels.Subcategory
{
    public class SubcategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ASP_FINAL.Models.Category Category { get; set; }
    }
}
