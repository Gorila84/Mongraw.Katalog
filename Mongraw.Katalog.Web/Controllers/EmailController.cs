using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mongraw.Katalog.Application.Service.Interfaces;
using Mongraw.Katalog.Domain.Models;

namespace Mongraw.Katalog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEMailService _eMailService;

        public EmailController(IEMailService eMailService)
        {
            _eMailService = eMailService;
        }

        [HttpPost("sendEmail")]
        public IActionResult SendEmail([FromBody] Email email)
        {
            email.To = "biuro@mongraw.pl";
            _eMailService.SendEmail(email);
            return Ok();
        }
    }
}
