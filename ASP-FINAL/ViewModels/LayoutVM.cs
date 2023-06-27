using ASP_FINAL.Models;

namespace ASP_FINAL.ViewModels
{
    public class LayoutVM
    {
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public Dictionary<string, string> SettingDatas { get; set; }
        public List<Category> Categories { get; set; }

    }
}
