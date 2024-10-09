namespace SearchPic_V2.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}