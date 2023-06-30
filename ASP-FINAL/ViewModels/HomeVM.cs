using ASP_FINAL.Models;

namespace ASP_FINAL.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Slider;
        public IEnumerable<Product> Products { get; set; }
        public Dictionary<string, string> SettingDatas { get; set; }
        public BlogVM Blog { get; set; }
        public List<Category> Categories { get; set; }
        public int ProductId { get; set; }

    }
}
