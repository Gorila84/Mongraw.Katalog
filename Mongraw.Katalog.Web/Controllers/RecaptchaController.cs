using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mongraw.Katalog.Domain.Models;

namespace Mongraw.Katalog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecaptchaController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public RecaptchaController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost("verifyRecaptcha")]
        public async Task<IActionResult> VerifyCaptcha(RecaptchaRequest request)
        {
            string requestUrl = "https://www.google.com/recaptcha/api/siteverify";

            var response = await _httpClient.PostAsync(requestUrl, new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", ""),
                new KeyValuePair<string, string>("response", request.Token)
            }));

            var result = await response.Content.ReadAsStringAsync();

            return Ok(result);
        }
    }
}
