using emil.Models.DTOs;
using emil.Services.IMail;
using MimeKit;
using MimeKit.Text;
namespace emil.Services
{
    public class GoogleMail : ISendMail
    {
        private readonly IConfiguration configuration;

        public GoogleMail(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Send(SendMailDTO sendMailDTO)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailSettings:EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(sendMailDTO.To));
            email.Subject = sendMailDTO.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = sendMailDTO.Body };
        }
    }
}
