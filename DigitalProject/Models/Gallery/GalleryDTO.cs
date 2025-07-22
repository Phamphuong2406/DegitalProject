using System.ComponentModel.DataAnnotations;

namespace DigitalProject.Models.Gallery
{
    public class GalleryDTO
    {
        public IFormFile ImageUrl { get; set; }
        [Required(ErrorMessage = "Bạn chưa điền tên địa điểm")]
        [StringLength(50, ErrorMessage = "Tên địa điểm không vượt quá 50 ký tự")]
        public string GalleryName { get; set; }
        public string Address { get; set; }
        public bool Displayorder { get; set; }
        public DateTime CreateAt { get; set; }
        public int PosterId { get; set; }
    }
    public class GetGalleryDTO
    {
        public string ImageUrl { get; set; }

        public string GalleryName { get; set; }
        public string Address { get; set; }
        public bool Displayorder { get; set; }
        public DateTime CreateAt { get; set; }
        public int PosterId { get; set; }
    }
}
