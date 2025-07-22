
using DigitalProject.Common.Paging;
using DigitalProject.Common.UploadFile;
using DigitalProject.Entitys;
using DigitalProject.Models.Gallery;
using DigitalProject.Repositories.Interface;
using DigitalProject.Services.Interface;

namespace DigitalProject.Services.Implements
{
    public class GalleryService: IGalleryService
    {
        private readonly IGalleryRepository _galleryRepo;
        public GalleryService(IGalleryRepository galleryRepo)
        {

            _galleryRepo = galleryRepo;
        }
        public List<Gallery> GetListgallery()
        {
            try
            {
                return _galleryRepo.getListGallery();
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public Gallery? getBygalleryId(int galleryId)
        {
            try
            {
                return _galleryRepo.GetGalleryById(galleryId);
            }
            catch (Exception)
            {
                throw;
            }
           
        }
        public PagingModel<GetGalleryDTO> getListGalleryByKeyword(string? address, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize)
        {
            try
            {

                return _galleryRepo.getListGalleryByKey(address, postingStartDate, postingEndDate,pageNumber,pageSize);

            }
            catch (Exception)
            {

                throw ;
            }
        }

        public void Addgallery(GalleryDTO model, int currentUserId)
        {
            try
            {
                var result = _galleryRepo.GetGalleryByName(model.GalleryName);
                var imageUrl = UploadHandler.Upload(model.ImageUrl);
                var gallery = new Gallery
                {
                   ImageUrl = imageUrl,
                   GalleryName = model.GalleryName,
                   Address = model.Address,
                   Displayorder = model.Displayorder,
                   CreateAt = DateTime.Now,
                    PosterId = currentUserId,
                };
                _galleryRepo.CreateGallery(gallery);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public bool Editgallery(GalleryDTO model, int galleryId)
        {
            try
            {

                var gallery = _galleryRepo.GetGalleryById(galleryId);
                if (gallery == null) return false;
                UploadHandler.DeleteFile(gallery.ImageUrl);
                gallery.ImageUrl = UploadHandler.Upload(model.ImageUrl);
                gallery.GalleryName = model.GalleryName;
                gallery.Address = model.Address;
                gallery.Displayorder = model.Displayorder;
                gallery.CreateAt = DateTime.Now;

                return _galleryRepo.Editgallery(gallery);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void Deletegallery(int galleryId)
        {
            try
            {
                var gallery = _galleryRepo.GetGalleryById(galleryId);
                _galleryRepo.Deletegallery(gallery);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
