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
        public string status { get; set; }
        public string note { get; set; }
        public string userResponse { get; set; }
        public DateTime respondedAt { get; set; }
        public string IpAddress { get; set; }
    }
}
