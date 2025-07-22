using AutoMapper;
using DigitalProject.Common.Paging;
using DigitalProject.Entitys;

namespace DigitalProject.Repositories.Implements
{
    public class ContactRequestRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public ContactRequestRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Gallery> getListGallery()
        {
            return _context.galleries.ToList();
        }
        public void AddGallery(Gallery model)
        {
            _context.galleries.Add(model);
            _context.SaveChanges();
        }
        public Gallery? GetByGalleryName(string galleryName)
        {
            return _context.galleries.FirstOrDefault(x => x.GalleryName == galleryName);
        }
        public Gallery? GetGalleryById(int id)
        {
            return _context.galleries.FirstOrDefault(x => x.PalleryId == id);
           
        }
        public bool Editgallery(Gallery model)
        {
            _context.galleries.Update(model);
            var result = _context.SaveChanges();
            return result > 0;
        }
        public void Deletegallery(Gallery model)
        {
            _context.galleries.Remove(model);
            _context.SaveChanges();
        }
        public bool UpdateRefreshToken(Gallery model)
        {
            _context.galleries.Update(model);
            var result = _context.SaveChanges();
            return result > 0;
        }
    }
}
