namespace ASP_FINAL.Models
{
    public class Basket : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<ProductBasket> ProductBaskets { get; set; }
    }
}
