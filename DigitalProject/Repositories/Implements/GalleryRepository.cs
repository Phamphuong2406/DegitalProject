
using DigitalProject.Entitys;
using DigitalProject.Repositories.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Digitalgallery.Repositories.Implements
{
    public class GalleryRepository: IGalleryRepository
    {
        private readonly MyDbContext _context;
        public GalleryRepository(MyDbContext context)
        {
            _context = context;

        }
        public List<Gallery> getListGallery()
        {
            return _context.galleries.Select(x => new Gallery
            {
                PalleryId = x.PalleryId,
                ImageUrl = x.ImageUrl,
                GalleryName = x.GalleryName,
                Address = x.Address,
                Displayorder = x.Displayorder,
                CreateAt = x.CreateAt,
                PosterId = x.PosterId,
            }).ToList();
        }
       public Gallery GetGalleryById(int galleryId)
        {
            var gallery = _context.galleries.FirstOrDefault(x => x.PalleryId == galleryId);
            if (gallery == null) { return null; }
            return gallery;
        }
        public bool GetGalleryByName(string galleryName)
        {
            var gallery = _context.galleries.FirstOrDefault(x => x.GalleryName == galleryName);
            if (gallery == null) { return false; } else { return true; }
        }
        public List<Gallery> getListGalleryByKey(string? address, DateTime? postingStartDate = null, DateTime? postingEndDate = null)
        {
            var query = _context.galleries.AsQueryable();
            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(z => z.Address.ToLower().Contains(address));
            }
            if (postingEndDate.HasValue && postingStartDate.HasValue)
            {
                var adjustedEnd = postingEndDate.Value.AddDays(1).AddMinutes(-1);
                query = query.Where(z => z.CreateAt >= postingStartDate && z.CreateAt <= adjustedEnd);
            }
            else if (postingStartDate.HasValue)
            {
                query = query.Where(z => z.CreateAt >= postingStartDate);
            }
            else if (postingEndDate.HasValue)
            {
                var adjustedEnd = postingEndDate.Value.AddDays(1).AddMinutes(-1);
                query = query.Where(z => z.CreateAt <= adjustedEnd);
            }
            return query.Select(x => new Gallery
            {
                PalleryId = x.PalleryId,
                ImageUrl = x.ImageUrl,
                GalleryName = x.GalleryName,
                Address = x.Address,
                Displayorder = x.Displayorder,
                CreateAt = x.CreateAt,
                PosterId = x.PosterId,
            }).ToList();

        }
        public void CreateGallery(Gallery gallery)
        {
            _context.galleries.Add(gallery);
            _context.SaveChanges();

        }
        public bool Editgallery(Gallery gallery)
        {
            _context.galleries.Update(gallery);
            var result = _context.SaveChanges();
            return result > 0;
        }
        public void Deletegallery(Gallery model)
        {
            _context.galleries.Remove(model);
            _context.SaveChanges();
        }
    }
}
