using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios;
using RombiBack.Services.ROM.SEGURIDAD.MGM_Accesos;

namespace RombiBack.Controllers.ROM.SEGURIDAD.MGM_Accesos
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesosController : ControllerBase
    {
        private readonly IAccesosServices _accesosHorariosServices;

        public AccesosController(IAccesosServices accesosServices)
        {
            _accesosHorariosServices = accesosServices;
        }

        [HttpGet("GetAccesos")]
        public async Task<IActionResult> GetAccesos()
        {

            var rpt = await _accesosHorariosServices.GetAccesos();
            return Ok(rpt);
        }
    }
}
