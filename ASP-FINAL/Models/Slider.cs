namespace ASP_FINAL.Models
{
    public class Slider : BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Descriptiom { get; set; }
        public bool Status { get; set; } = true;
    }
}
