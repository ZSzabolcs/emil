using emil.Models.DTOs;

namespace emil.Services.IMail
{
    public interface ISendMail
    {
        Task SendAsync(SendMailDTO sendMailDTO);
    }
}