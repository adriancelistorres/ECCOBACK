using RombiBack.Entities.ROM.SEGURIDAD.Models.Accesos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.SEGURIDAD.MGM_Perfiles
{
    public interface IPerfilesRepository
    {
        Task<List<Accesos>> GetPerfiles();

    }
}
