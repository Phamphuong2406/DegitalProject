using AutoMapper;
using Azure;
using DigitalProject.Common.Paging;
using DigitalProject.Common.UploadFile;
using DigitalProject.Entitys;
using DigitalProject.Models.ContactRequest;
using DigitalProject.Repositories.Interface;
using DigitalProject.Services.Interface;

namespace DigitalProject.Services.Implements
{
    public class ContactRequestService: IContactRequestService
    {
        private readonly IContactRequestRepository _requestRepo;
        private readonly IMapper _mapper;
        public ContactRequestService(IContactRequestRepository requestRepo, IMapper mapper )
        {
            _mapper = mapper;
            _requestRepo = requestRepo;
        }
        public List<ContactRequest> GetListContactRequest()
        {
            try
            {
                return _requestRepo.GetListContactRequest();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ContactRequest? GetByContactRequestId(int requestId)
        {
            try
            {
                return _requestRepo.FindById(requestId);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public PagingModel<GetRequestDTO> GetListContactRequestByKeyword(string? key, DateTime? requestDate, string? status, int pageNumber, int pageSize)
        {
            try
            {

                return _requestRepo.GetListRequestByKey(key, requestDate,status, pageNumber,pageSize);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AddContactRequest(CreateRequestDTO model, int currentUserId, string idAddress)
        {
            try
            {
                var newRequest = _mapper.Map<ContactRequest>(model);
                newRequest.Status = "UnProcessed";
                newRequest.RequestType = "Comments";
                newRequest.RequestTime = DateTime.Now;
                newRequest.IpAddress = idAddress;
                _requestRepo.CreateContactRequest(newRequest);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void UpdateContactRequest(UpdateRequestDTO model, int requestId)
        {
            try
            {
                var request = _requestRepo.FindById(requestId);
               
                    request.CustommerName = model.CustommerName;
                    request.CustomerPhoneNumber = model.CustomerPhoneNumber;
                    request.CustomerEmail = model.CustomerEmail;
                    request.CustomerMessage = model.CustomerMessage;
                    request.RequestType = model.RequestType;
                    _requestRepo.EditContactRequest(request);
                  
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void UpdateResponseStatus(UpdateResponseStatusDTO model, int requestId, int respondentId)
        {
            try
            {
                var request = _requestRepo.FindById(requestId);
                request.Status = model.Status;
                request.Note = model.Note;
                request.RespondentId = respondentId;
                request.ResponseTime = DateTime.Now;
                _requestRepo.EditContactRequest(request);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteContactRequest(int requestId)
        {
            try
            {
                var request = _requestRepo.FindById(requestId);
                _requestRepo.DeleteContactRequest(request);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
