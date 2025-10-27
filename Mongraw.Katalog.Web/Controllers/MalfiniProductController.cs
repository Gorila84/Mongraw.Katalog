using Microsoft.AspNetCore.Mvc;
using Mongraw.Katalog.Application.Helpers;
using Mongraw.Katalog.Application.Service.Interfaces;
using Mongraw.Katalog.Domain.Interfaces;

namespace Mongraw.Katalog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MalfiniProductController : ControllerBase
    {
        private readonly IMalfiniProductService _service;

        public MalfiniProductController(IMalfiniProductService service)
        {
            _service = service;
        }

        [HttpGet("getMalfiniProducts")]
        public async Task<IActionResult> GetMalfiniProducts([FromQuery] ProductParams productParams)
        {
            var result = await _service.GetPagedMalfiniProductsAsync(productParams);
            return Ok(result);
        }
    }
}
