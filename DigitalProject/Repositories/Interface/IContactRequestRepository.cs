using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.ContactRequest;
using Microsoft.EntityFrameworkCore;

namespace DigitalProject.Repositories.Interface
{
    public interface IContactRequestRepository
    {
        List<ContactRequest> GetListContactRequest();

        ContactRequest? FindById(int requestId);

        PagingModel<GetRequestDTO> GetListRequestByKey(string? key, DateTime? requestDate, string? status, int pageNumber, int pageSize);

       void CreateContactRequest(ContactRequest request);

        void EditContactRequest(ContactRequest request);

        void DeleteContactRequest(ContactRequest request);
        
    }
}
