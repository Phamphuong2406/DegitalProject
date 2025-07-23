
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
        public List<Gallery> GetListGallery()
        {
            return _context.galleries.ToList();
        }
       public Gallery? FindById(int galleryId)
        {
            return _context.galleries.FirstOrDefault(x => x.GalleryId == galleryId);
           
        }
        public void FindByName(string galleryName)
        {
            _context.galleries.FirstOrDefault(x => x.GalleryName == galleryName);
        }
        public PagingModel<GetGalleryDTO> GetListGalleryByKey(string? address, DateTime? postingStartDate, DateTime? postingEndDate, int pageNumber, int pageSize)
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
        public void EditGallery(Gallery gallery)
        {
            _context.galleries.Update(gallery);
             _context.SaveChanges();
        }
        public void DeleteGallery(Gallery model)
        {
            _context.galleries.Remove(model);
            _context.SaveChanges();
        }
    }
}
