﻿using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Accesos;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Perfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Services.ROM.SEGURIDAD.MGM_Accesos
{
    public interface IAccesosServices
    {
        Task<List<Accesos>> GetAccesos();
        Task<Respuesta> PostAccesos(AccesosRequest accs);
        Task<Respuesta> DeleteAccesos(AccesosRequest accs);

        Task<Accesos> GetSegUsuario(string usuario);
        Task<List<Perfiles>> GetPerfiles();

    }
}
