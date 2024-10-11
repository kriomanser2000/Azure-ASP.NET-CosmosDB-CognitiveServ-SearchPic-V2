namespace SearchPic_V2.Models
{
    public class Tag
    {
        public int TagId { get; set; }  // Первинний ключ
        public string TagName { get; set; }

        // Зовнішній ключ для зображення
        public int ImageId { get; set; }

        // Навігаційна властивість для зв'язку з класом Image
        public Image Image { get; set; }
    }
}