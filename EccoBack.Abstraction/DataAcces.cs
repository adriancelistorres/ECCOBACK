using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EccoBack.Abstraction
{
    public class DataAcces
    {
        private readonly string _connectionString;
        private readonly string _connectionStringBI;

        public DataAcces(IConfiguration configuracion)
        {
            _connectionString = configuracion.GetConnectionString("ENTEL");
            _connectionStringBI = configuracion.GetConnectionString("APP_BI");
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
