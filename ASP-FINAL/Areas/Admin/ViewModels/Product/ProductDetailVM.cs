using ASP_FINAL.Models;

namespace ASP_FINAL.Areas.Admin.ViewModels.Product
{
    public class ProductDetailVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductImage> Image { get; set; }
        public int SKU { get; set; }
        public int SalesCount { get; set; }
        public int StockCount { get; set; }
        public string Price { get; set; }
        public string CreateDate { get; set; }
        public string Brand { get; set; }
        public Subcategory Subcategory { get; set; }
        public Discount Discount { get; set; }
        public int Rating { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductWishList> ProductWishlists { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<ProductBasket> ProductBaskets { get; set; }
        public string Category { get; set; } // Add the Category property
    }
}
