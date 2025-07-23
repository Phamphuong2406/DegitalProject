using DigitalProject.Common.Paging;
using DigitalProject.Entitys;
using DigitalProject.Models.ContactRequest;

namespace DigitalProject.Services.Interface
{
    public interface IContactRequestService
    {
        List<ContactRequest> GetListContactRequest();

        ContactRequest? GetByContactRequestId(int requestId);
        PagingModel<GetRequestDTO> GetListContactRequestByKeyword(string? key, DateTime? requestDate, string? status, int pageNumber, int pageSize);

        void AddContactRequest(CreateRequestDTO model, int currentUserId, string idAddress);
        void UpdateContactRequest(UpdateRequestDTO model, int requestId);
        void UpdateResponseStatus(UpdateResponseStatusDTO model, int requestId, int respondentId);
        void DeleteContactRequest(int requestId);
    }
}
