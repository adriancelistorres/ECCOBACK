using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Security.Model.UserAuth;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Reports;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanificacionHorariosController : ControllerBase
    {
        private readonly IPlanificaionHorariosServices _planificacionHorariosServices;

        public PlanificacionHorariosController(IPlanificaionHorariosServices planificacionHorariosServices)
        {
            _planificacionHorariosServices = planificacionHorariosServices;
        }

        [HttpPost("GetTurnosSupervisor")]
        public async Task<IActionResult> GetTurnosSupervisor([FromBody] TurnosSupervisor userdata)
        {

            var turnosSupervisors = await _planificacionHorariosServices.GetTurnosSupervisor(userdata.usuario);
            return Ok(turnosSupervisors);
        }

        [HttpPost("PostTurnosSupervisor")]
        public async Task<IActionResult> PostTurnosSupervisor([FromBody] TurnosSupervisorRequest turnos)
        {
            var respuesta= await _planificacionHorariosServices.PostTurnosSupervisor(turnos);
            return Ok(respuesta);
        }
    }
}
