using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Gallery;

namespace DigitalProject.Services.Interface
{
    public interface IGalleryService
    {
        List<Gallery> GetListgallery();

        Gallery? getBygalleryId(int galleryId);
        PagingModel<GetGalleryDTO> getListGalleryByKeyword(string? address, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize);
        void Addgallery(GalleryDTO model, int currentUserId);
        bool Editgallery(GalleryDTO model, int galleryId);
        void Deletegallery(int galleryId);
        
    }
}
