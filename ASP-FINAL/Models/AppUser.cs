using Microsoft.AspNetCore.Identity;

namespace ASP_FINAL.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public Basket Basket { get; set; }
        public WishList WishList { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
