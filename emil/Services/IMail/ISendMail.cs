using emil.Models.DTOs;

namespace emil.Services.IMail
{
    public interface ISendMail
    {
        void Send(SendMailDTO sendMailDTO);
    }
}
