using AutoMapper;
using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.ContactRequest;
using DigitalProject.Models.Gallery;
using DigitalProject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DigitalProject.Repositories.Implements
{
    public class ContactRequestRepository: IContactRequestRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public ContactRequestRepository(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<ContactRequest> GetListContactRequest()
        {
            return _context.contactRequests.ToList();
        }
        public ContactRequest? FindById(int requestId)
        {
            return _context.contactRequests.FirstOrDefault(x => x.RequestId == requestId);

        }
        public PagingModel<GetRequestDTO> GetListRequestByKey(string? key, DateTime? requestDate, string? status, int pageNumber, int pageSize)
        {
            var query = _context.contactRequests.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(z => z.CustommerName.ToLower().Contains(key) || z.CustomerPhoneNumber.Contains(key));
            }

            if (requestDate.HasValue)
            {
                query = query.Where(z => z.RequestTime.Date == requestDate);
            }
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(z => z.Status == status);
            }
            var totalRecords = query.Count();
            var pagedData = query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var data = _mapper.Map<List<GetRequestDTO>>(pagedData);
            return new PagingModel<GetRequestDTO>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Data = data,
                TotalRecords = totalRecords
            };

        }
        public void CreateContactRequest(ContactRequest request)
        {
            _context.contactRequests.Add(request);
            _context.SaveChanges();

        }
        public void EditContactRequest(ContactRequest request)
        {
            _context.contactRequests.Update(request);
            _context.SaveChanges();
        }
        public void DeleteContactRequest(ContactRequest request)
        {
            _context.contactRequests.Remove(request);
            _context.SaveChanges();
        }
    }
}
