using System.ComponentModel.DataAnnotations;

namespace DigitalProject.Models.ContactRequest
{
    public class ContactRequestDTO
    {

    }
    public class GetRequestDTO
    {
        public int RequestId { get; set; }
        public string CustommerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMessage { get; set; }
        public string RequestType { get; set; }
        public DateTime RequestTime { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }
        public int? RespondentId { get; set; }
        public DateTime? ResponseTime { get; set; }
        public string IpAddress { get; set; }
    }
    public class CreateRequestDTO()
    {
       [ Required(ErrorMessage = "Vui lòng điền tên đầy đủ")]
        [StringLength(20, ErrorMessage = "Tên không được vượt quá 20 ký tự.")]
        public string CustommerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMessage { get; set; }
        public string RequestType { get; set; }
        public DateTime RequestTime { get; set; }
        public string Status { get; set; }
        public string IpAddress { get; set; }
    }
    public class UpdateRequestDTO()
    {
        public string? CustommerName { get; set; }
        public string? CustomerPhoneNumber { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerMessage { get; set; }
        public string? RequestType { get; set; }
      
    }
    public class UpdateResponseStatusDTO()
    {
        public string Status { get; set; }
        public string? Note { get; set; }
        public int? RespondentId { get; set; }
        public DateTime ResponseTime { get; set; }
    }
}
