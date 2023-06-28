namespace ASP_FINAL.Areas.Admin.ViewModels.Subcategory
{
    public class SubcategoryEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Models.Category> Category { get; set; }
        public int CategoryId { get; set; }

    }
}
