
using DigitalProject.Common.Paging;
using DigitalProject.Common.UploadFile;
using DigitalProject.Entitys;
using DigitalProject.Models.Gallery;
using DigitalProject.Repositories.Interface;
using DigitalProject.Services.Interface;

namespace DigitalProject.Services.Implements
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository _galleryRepo;
        public GalleryService(IGalleryRepository galleryRepo)
        {

            _galleryRepo = galleryRepo;
        }
        public List<Gallery> GetListGallery()
        {
            try
            {
                return _galleryRepo.GetListGallery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Gallery? GetByGalleryId(int galleryId)
        {
            try
            {
                return _galleryRepo.FindById(galleryId);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public PagingModel<GetGalleryDTO> GetListGalleryByKeyword(string? address, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize)
        {
            try
            {

                return _galleryRepo.GetListGalleryByKey(address, postingStartDate, postingEndDate, pageNumber, pageSize);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddGallery(GalleryDTO model, int currentUserId)
        {
            try
            {
                _galleryRepo.FindByName(model.GalleryName);
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
        public void EditGallery(GalleryDTO model, int galleryId)
        {
            try
            {

                var gallery = _galleryRepo.FindById(galleryId);
                UploadHandler.DeleteFile(gallery.ImageUrl);
                gallery.ImageUrl = UploadHandler.Upload(model.ImageUrl);
                gallery.GalleryName = model.GalleryName;
                gallery.Address = model.Address;
                gallery.Displayorder = model.Displayorder;
                gallery.CreateAt = DateTime.Now;

                _galleryRepo.EditGallery(gallery);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void DeleteGallery(int galleryId)
        {
            try
            {
                var gallery = _galleryRepo.FindById(galleryId);
                _galleryRepo.DeleteGallery(gallery);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
