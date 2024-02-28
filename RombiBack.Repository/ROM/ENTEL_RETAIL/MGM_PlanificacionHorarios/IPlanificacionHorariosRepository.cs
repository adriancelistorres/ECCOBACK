using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.LOGIN.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios
{
    public interface IPlanificacionHorariosRepository
    {
        Task<List<TurnosSupervisor>> GetTurnosSupervisor(string usuario);
        Task<Respuesta> PostTurnosSupervisor(TurnosSupervisorRequest turnossuper);


    }
}
