using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_ValidacionBundles;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_ValidacionBundles
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidacionBundlesController : ControllerBase
    {
        private readonly IValidacionBundlesServices _validacionBundlesServices;

        public ValidacionBundlesController(IValidacionBundlesServices validacionBundlesServices)
        {
            _validacionBundlesServices = validacionBundlesServices;
        }

        [HttpPost("GetBundlesVentas")]
        public async Task<IActionResult> GetBundlesVentas([FromBody] int intIdVentasPrincipal)
        {
            var rptabundle = await _validacionBundlesServices.GetBundlesVentas(intIdVentasPrincipal);
            return Ok(rptabundle);
        }

    }
}
