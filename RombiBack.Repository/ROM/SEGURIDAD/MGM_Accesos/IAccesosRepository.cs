using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Accesos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.SEGURIDAD.MGM_Accesos
{
    public interface IAccesosRepository
    {
        Task<List<Accesos>> GetAccesos();
        Task<Respuesta> PostAccesos(AccesosRequest accs);

    }
}
