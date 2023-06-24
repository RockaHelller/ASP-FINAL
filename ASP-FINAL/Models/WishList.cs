namespace ASP_FINAL.Models
{
    public class WishList : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<ProductWishList> ProductWishLists { get; set; }

    }
}
