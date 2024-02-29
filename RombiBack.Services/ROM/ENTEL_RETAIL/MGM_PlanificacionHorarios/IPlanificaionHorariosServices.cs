using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios
{
    public interface IPlanificaionHorariosServices
    {
        Task<List<TurnosSupervisor>> GetTurnosSupervisor(string usuario);
        Task<Respuesta> PostTurnosSupervisor(TurnosSupervisorRequest turnossuper);
        Task<Respuesta> PutTurnosSupervisor(TurnosSupervisor turnossuper);
        Task<Respuesta> DeleteTurnosSupervisor(TurnosSupervisor turnossuper);


        Task<List<SupervisorPdvResponse>> GetSupervisorPDV(string usuario);
        Task<List<TurnosSupervisor>> GetTurnosDisponiblePDV(TurnosDisponiblesPdvRequest turnodispo);
        Task<Respuesta> PostTurnosPDV(TurnosPdvRequest turnospdv);
        Task<List<TurnosSupervisor>> GetTurnosAsignadosPDV(TurnosDisponiblesPdvRequest turnodispo);
        Task<Respuesta> DeleteTurnosPDV(TurnosPdvRequest turnospdv);


    }
}
