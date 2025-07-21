using DigitalProject.Entitys;

namespace DigitalProject.Repositories.Interface
{
    public interface IGalleryRepository
    {
        List<Gallery> getListGallery();
        Gallery GetGalleryById(int galleryId);
        bool GetGalleryByName(string galleryName);
        List<Gallery> getListGalleryByKey(string? address, DateTime? postingStartDate = null, DateTime? postingEndDate = null);
        void CreateGallery(Gallery gallery);
        bool Editgallery(Gallery gallery);
        void Deletegallery(Gallery model);
    }
}
