namespace DigitalProject.Entitys
{
    public class Gallery
    {
        public int GalleryId { get; set; }
        public string ImageUrl { get; set; }
        public string GalleryName { get; set; }
        public string Address { get; set; }
        public bool Displayorder { get; set; } 
        public DateTime CreateAt { get; set; }
        public int PosterId { get; set; }
        public User users { get; set; }
    }
}
