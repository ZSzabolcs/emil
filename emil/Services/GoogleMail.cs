using emil.Models.DTOs;
using emil.Services.IMail;
using MailKit.Net.Smtp;
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

            using var smtp = new SmtpClient();
            smtp.Connect(configuration.GetSection("EmailSettings:EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration.GetSection("EmailSettings:EmailUserName").Value, configuration.GetSection("EmailSettings:EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
