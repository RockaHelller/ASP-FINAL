using Microsoft.AspNetCore.Identity;

namespace ASP_FINAL.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
