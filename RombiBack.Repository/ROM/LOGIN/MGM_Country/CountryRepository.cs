using RombiBack.Abstraction;
using RombiBack.Entities.ROM.LOGIN.Company;
using RombiBack.Entities.ROM.LOGIN.Country;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.LOGIN.MGM_Country
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataAcces _dbConnection;

        public CountryRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
      

        public async Task<List<Country>> GetAll()
        {
            List<Country> countries = new List<Country>();

            try
            {
                using (SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionAPP_BI()))
                {
                    await sql.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("sp_getCountry", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Country country = new Country
                                {
                                    COD_PAIS = reader["COD_PAIS"] as string,
                                    DESCRIPCION = reader["DESCRIPCION"] as string,
                                    VISIBLE = reader["VISIBLE"] != DBNull.Value && Convert.ToBoolean(reader["VISIBLE"]),
                                    ESTADO = reader["ESTADO"] != DBNull.Value && Convert.ToBoolean(reader["ESTADO"]),
                                };
                                countries.Add(country);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario (registrar, relanzar, etc.)
            }

            return countries;
        }




       
    }
}
