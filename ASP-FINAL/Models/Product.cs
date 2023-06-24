using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_FINAL.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int SKUCode { get; set; }
        public int SalesCount { get; set; }
        public int StockCount { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        [ForeignKey("Subcategory")]
        public int SubcategoryId { get; set; }
        public Subcategory SubCategory { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int RatingId { get; set; }
        public Rating Rating { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
        public ICollection<ProductWishList> ProductWishLists { get; set; }
        public ICollection<ProductBasket> ProductBaskets { get; set; }
    }
}
