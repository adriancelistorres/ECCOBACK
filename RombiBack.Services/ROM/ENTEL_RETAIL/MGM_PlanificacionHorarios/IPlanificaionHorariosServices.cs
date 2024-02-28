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
        Task<string> PostTurnosSupervisor(TurnosSupervisorRequest turnossuper);


    }
}
