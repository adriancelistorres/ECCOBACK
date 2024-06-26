﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Accesos;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios;
using RombiBack.Services.ROM.SEGURIDAD.MGM_Accesos;

namespace RombiBack.Controllers.ROM.SEGURIDAD.MGM_Accesos
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccesosController : ControllerBase
    {
        private readonly IAccesosServices _accesosServices;

        public AccesosController(IAccesosServices accesosServices)
        {
            _accesosServices = accesosServices;
        }

        [HttpGet("GetAccesos")]
        public async Task<IActionResult> GetAccesos()
        {

            var rpt = await _accesosServices.GetAccesos();
            return Ok(rpt);
        }

        [HttpPost("PostAccesos")]
        public async Task<IActionResult> PostAccesos([FromBody] AccesosRequest accs)
        {
            var rpt = await _accesosServices.PostAccesos(accs);
            return Ok(rpt);
        }

        [HttpPost("DeleteAccesos")]
        public async Task<IActionResult> DeleteAccesos([FromBody] AccesosRequest accs)
        {
            var rpt = await _accesosServices.DeleteAccesos(accs);
            return Ok(rpt);
        }

        [HttpPost("GetSegUsuario")]
        public async Task<IActionResult> GetSegUsuario([FromBody] string usuario)
        {
            var rpt = await _accesosServices.GetSegUsuario(usuario);
            return Ok(rpt);
        }

        [HttpGet("GetPerfiles")]
        public async Task<IActionResult> GetPerfiles()
        {

            var rpt = await _accesosServices.GetPerfiles();
            return Ok(rpt);
        }

    }
}
