using emil.Data;
using emil.Models;
using emil.Models.DTOs;
using emil.Services.IMail;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace emil.Services
{
    public class GoogleMail : ISendMail
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public GoogleMail(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task SendAsync(SendMailDTO sendMailDTO)
        {
            var senderEmail = _configuration.GetSection("EmailSettings:EmailUserName").Value;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(senderEmail));
            email.To.Add(MailboxAddress.Parse(sendMailDTO.To));
            email.Subject = sendMailDTO.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = sendMailDTO.Body };

            // 1. Küldés
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration.GetSection("EmailSettings:EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(senderEmail, _configuration.GetSection("EmailSettings:EmailPassword").Value);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);

            // 2. Mentés adatbázisba
            var record = new SentEmail
            {
                Sender = senderEmail,
                Recipient = sendMailDTO.To,
                Subject = sendMailDTO.Subject,
                Body = sendMailDTO.Body,
                SentDate = DateTime.Now
            };

            _context.SentEmails.Add(record);
            await _context.SaveChangesAsync();
        }
    }
}