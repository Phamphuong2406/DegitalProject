
using AutoMapper;
using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.Gallery;
using DigitalProject.Repositories.Interface;

namespace Digitalgallery.Repositories.Implements
{
    public class GalleryRepository: IGalleryRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public GalleryRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Gallery> getListGallery()
        {
            return _context.galleries.ToList();
        }
       public Gallery? GetGalleryById(int galleryId)
        {
            return _context.galleries.FirstOrDefault(x => x.PalleryId == galleryId);
           
        }
        public bool GetGalleryByName(string galleryName)
        {
            var gallery = _context.galleries.FirstOrDefault(x => x.GalleryName == galleryName);
            return gallery != null;
        }
        public PagingModel<GetGalleryDTO> getListGalleryByKey(string? address, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize)
        {
            var query = _context.galleries.AsQueryable();
            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(z => z.Address.ToLower().Contains(address));
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
            var totalRecords = query.Count();
            var pagedData = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var data = _mapper.Map<List<GetGalleryDTO>>(pagedData);
            return new PagingModel<GetGalleryDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = data,
                TotalRecords = totalRecords
            };

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
