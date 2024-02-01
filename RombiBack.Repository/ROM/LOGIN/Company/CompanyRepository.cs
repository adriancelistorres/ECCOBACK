using RombiBack.Abstraction;
using RombiBack.Entities.ROM.LOGIN.Company;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.LOGIN.Company
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DataAcces _dbConnection;

        public CompanyRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<List<Companys>> GetCompany()
        {
            List<Companys> companies = new List<Companys>();

            using (SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionAPP_BI()))
            {
                // Abre la conexión antes de usarla
                await sql.OpenAsync();

                // Crea y ejecuta la consulta SQL
                string query = "SELECT EMPRESAID, EMPRESA, RUC, DIRECCION_EMPRESA FROM [SEG_EMPRESA]";
                using (SqlCommand cmd = new SqlCommand(query, sql))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        // Comprueba si hay filas devueltas
                        if (reader.HasRows)
                        {
                            // Itera sobre las filas y mapea los resultados a la lista de empresas
                            while (await reader.ReadAsync())
                            {
                                Companys company = new Companys
                                {
                                    EMPRESAID = reader["EMPRESAID"] != DBNull.Value ? reader["EMPRESAID"].ToString() : null,
                                    EMPRESA = reader["EMPRESA"] != DBNull.Value ? reader["EMPRESA"].ToString() : null,
                                    RUC = reader["RUC"] != DBNull.Value ? reader["RUC"].ToString() : null,
                                    DIRECCION_EMPRESA = reader["DIRECCION_EMPRESA"] != DBNull.Value ? reader["DIRECCION_EMPRESA"].ToString() : null
                                };
                                companies.Add(company);
                            }
                        }
                    }
                }
            }

            return companies;
        }
    }
}


