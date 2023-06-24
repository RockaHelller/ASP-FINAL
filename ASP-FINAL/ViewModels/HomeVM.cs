using ASP_FINAL.Models;

namespace ASP_FINAL.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Slider;
        public IEnumerable<Product> Products { get; set; }
    }
}
