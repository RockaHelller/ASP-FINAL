namespace ASP_FINAL.Areas.Admin.ViewModels
{
    public class SliderEditVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public List<IFormFile> NewImage { get; set; }
        public string NewTitle { get; set; }
        public string NewDesc { get; set; }
    }
}
