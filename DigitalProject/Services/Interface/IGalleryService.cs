using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Gallery;

namespace DigitalProject.Services.Interface
{
    public interface IGalleryService
    {
        List<Gallery> GetListGallery();

        Gallery? GetByGalleryId(int galleryId);
        PagingModel<GetGalleryDTO> GetListGalleryByKeyword(string? address, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize);
        void AddGallery(GalleryDTO model, int currentUserId);
        void EditGallery(GalleryDTO model, int galleryId);
        void DeleteGallery(int galleryId);
        
    }
}
