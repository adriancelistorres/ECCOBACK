using RombiBack.Abstraction;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Accesos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.SEGURIDAD.MGM_Perfiles
{
    public class PerfilesRepository:IPerfilesRepository
    {
        private readonly DataAcces _dbConnection;

        public PerfilesRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<List<Accesos>> GetPerfiles()
        {
            try
            {


                return null;
            }
            catch {
                return null;
            }

        }
    }
}
