using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Gallery;

namespace DigitalProject.Repositories.Interface
{
    public interface IGalleryRepository
    {
        List<Gallery> getListGallery();
        Gallery? GetGalleryById(int galleryId);
        bool GetGalleryByName(string galleryName);
        PagingModel<GetGalleryDTO> getListGalleryByKey(string? address, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize);
        void CreateGallery(Gallery gallery);
        bool Editgallery(Gallery gallery);
        void Deletegallery(Gallery model);
    }
}
