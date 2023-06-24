namespace ASP_FINAL.Models
{
    public class Rating : BaseEntity
    {
        public byte RatingCount { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
