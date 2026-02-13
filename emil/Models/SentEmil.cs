namespace emil.Models
{
    public class SentEmail
    {
        public int Id { get; set; }
        public string? Sender { get; set; }
        public string? Recipient { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime SentDate { get; set; }
    }
}
