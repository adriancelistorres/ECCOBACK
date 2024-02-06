using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Abstraction
{
    public class DataAcces
    {
        private readonly string _connectionStringENTEL_RETAIL;
        private readonly string _connectionStringAPP_BI;
        private readonly string _connectionStringROMBI;


        public DataAcces(IConfiguration configuracion)
        {
            _connectionStringENTEL_RETAIL = configuracion.GetConnectionString("ENTEL_RETAIL");
            _connectionStringAPP_BI = configuracion.GetConnectionString("APP_BI");
            _connectionStringROMBI = configuracion.GetConnectionString("ROMBI");

        }

        public string GetConnectionENTEL_RETAIL()
        {
            return _connectionStringENTEL_RETAIL;
        }

        public string GetConnectionAPP_BI()
        {
            return _connectionStringAPP_BI;
        }

        public string GetConnectionROMBI()
        {
            return _connectionStringROMBI;
        }
    }
}
