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

        [HttpPost("PutTurnosSupervisor")]
        public async Task<IActionResult> PutTurnosSupervisor([FromBody] TurnosSupervisor turnos)
        {
            var respuesta = await _planificacionHorariosServices.PutTurnosSupervisor(turnos);
            return Ok(respuesta);
        }

        [HttpPost("DeleteTurnosSupervisor")]
        public async Task<IActionResult> DeleteTurnosSupervisor([FromBody] TurnosSupervisor turnos)
        {
            var respuesta = await _planificacionHorariosServices.DeleteTurnosSupervisor(turnos);
            return Ok(respuesta);
        }


        [HttpPost("GetSupervisorPDV")]
        public async Task<IActionResult> GetSupervisorPDV([FromBody] TurnosSupervisor userdata)
        {

            var pdvsupervisor = await _planificacionHorariosServices.GetSupervisorPDV(userdata.usuario);
            return Ok(pdvsupervisor);
        }



        [HttpPost("GetTurnosDisponiblePDV")]
        public async Task<IActionResult> GetTurnosDisponiblePDV([FromBody] TurnosDisponiblesPdvRequest turnosdispopdv)
        {
            var turnosdispo = await _planificacionHorariosServices.GetTurnosDisponiblePDV(turnosdispopdv);
            return Ok(turnosdispo);
        }


        [HttpPost("PostTurnosPDV")]
        public async Task<IActionResult> PostTurnosPDV([FromBody] TurnosPdvRequest turnospdv)
        {
            var turnospdvres = await _planificacionHorariosServices.PostTurnosPDV(turnospdv);
            return Ok(turnospdvres);
        }



        [HttpPost("GetTurnosAsignadosPDV")]
        public async Task<IActionResult> GetTurnosAsignadosPDV([FromBody] TurnosDisponiblesPdvRequest turnosasig)
        {
            var turnosasignados = await _planificacionHorariosServices.GetTurnosAsignadosPDV(turnosasig);
            return Ok(turnosasignados);
        }



    }
}
