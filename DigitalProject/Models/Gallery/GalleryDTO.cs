namespace DigitalProject.Models.Gallery
{
    public class GalleryDTO
    {
        public IFormFile ImageUrl { get; set; }
        public string GalleryName { get; set; }
        public string Address { get; set; }
        public bool Displayorder { get; set; }
        public DateTime CreateAt { get; set; }
        public int PosterId { get; set; }
    }
}
