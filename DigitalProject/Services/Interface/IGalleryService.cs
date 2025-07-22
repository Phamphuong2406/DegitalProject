using DigitalProject.Entitys;
using DigitalProject.Models.Gallery;

namespace DigitalProject.Services.Interface
{
    public interface IGalleryService
    {
        List<Gallery> GetListgallery();

        Gallery getBygalleryId(int galleryId);
        List<Gallery> getListGalleryByKeyword(string? address, DateTime? postingStartDate = null, DateTime? postingEndDate = null);
        bool Addgallery(GalleryDTO model, int currentUserId);
        bool Editgallery(GalleryDTO model, int galleryId);
        bool Deletegallery(int galleryId);
        
    }
}
