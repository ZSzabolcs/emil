using emil.Models.DTOs;
using emil.Services.IMail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace emil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly ISendMail _send;

        public SendMailController(ISendMail send)
        {
            _send = send;
        }

        [HttpPost]
        public ActionResult SendMail(SendMailDTO sendMailDTO)
        {
            _send.Send(sendMailDTO);
            return Ok(new { Result = "Sikeres küldés" });
        }
    }
}
