
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
            return _galleryRepo.getListGallery();
        }
       
        public Gallery getBygalleryId(int galleryId)
        {
            var gallery = _galleryRepo.GetGalleryById(galleryId);
            if (gallery == null)
            {
                return null;
            }
            return gallery;
        }
        public List<Gallery> getListGalleryByKeyword(string? address, DateTime? postingStartDate = null, DateTime? postingEndDate = null)
        {
            try
            {

                return _galleryRepo.getListGalleryByKey(address, postingStartDate, postingEndDate);

            }
            catch (Exception)
            {

                throw new ApplicationException("Có lỗi xảy ra khi gọi danh sách bài viết");
            }
        }

        public bool Addgallery(GalleryDTO model, int currentUserId)
        {
            try
            {
                var result = _galleryRepo.GetGalleryByName(model.GalleryName);
                var imageUrl = UploadHandler.Upload(model.ImageUrl);
                if (result == true)
                {
                    return false;
                }
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
                return true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool Editgallery(GalleryDTO model, int galleryId)
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
        public bool Deletegallery(int galleryId)
        {
            var gallery = _galleryRepo.GetGalleryById(galleryId);
            if (gallery == null) return false;
            _galleryRepo.Deletegallery(gallery);
            return true;
        }
    }
}
