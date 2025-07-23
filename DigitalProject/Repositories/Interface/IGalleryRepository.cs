using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Gallery;

namespace DigitalProject.Repositories.Interface
{
    public interface IGalleryRepository
    {
        List<Gallery> GetListGallery();
        Gallery? FindById(int galleryId);
        void FindByName(string galleryName);
        PagingModel<GetGalleryDTO> GetListGalleryByKey(string? address, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize);
        void CreateGallery(Gallery gallery);
        void EditGallery(Gallery gallery);
        void DeleteGallery(Gallery model);
    }
}
