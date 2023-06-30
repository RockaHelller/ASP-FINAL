using ASP_FINAL.Models;

namespace ASP_FINAL.Areas.Admin.ViewModels.Product
{
    public class ProductEditVM
    {
        public int Id { get; set; }
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
        public List<IFormFile> Image { get; set; }
        //public List<ProductImage> Images { get; set; }
        //public string ImageName { get; set; } // Add this property to store the image name
        public List<TagCheckBox> Tags { get; set; }
    }


}
