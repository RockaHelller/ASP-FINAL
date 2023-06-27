using System.ComponentModel.DataAnnotations;

namespace ASP_FINAL.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
