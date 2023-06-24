namespace ASP_FINAL.Models
{
    public class ProductWishList
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WishListId { get; set; }
        public WishList WishList { get; set; }
    }
}
