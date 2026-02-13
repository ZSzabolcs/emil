using emil.Models.DTOs;
using emil.Services.IMail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace emil.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SendMailController : ControllerBase
    {
        private readonly ISendMail _send;
        public SendMailController(ISendMail send) => _send = send;

        [HttpPost]
        public async Task<ActionResult> SendMail(SendMailDTO sendMailDTO)
        {
            await _send.SendAsync(sendMailDTO);
            return Ok(new { Result = "Sikeres küldés és mentés!" });
        }
    }
}