namespace DigitalProject.Entitys
{
    public class ContactRequest
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
        public User users { get; set; }
    }
}
