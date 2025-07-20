namespace DigitalProject.Entitys
{
    public class Gallery
    {
        public int  PalleryId { get; set; }
        public string ImageUrl { get; set; }
        public string galleryName { get; set; }
        public string Address { get; set; }
        public bool displayorder { get; set; } 
        public DateTime createAt { get; set; }
        public int PosterId { get; set; }
    }
}
