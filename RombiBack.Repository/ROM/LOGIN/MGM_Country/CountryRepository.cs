using RombiBack.Abstraction;
using RombiBack.Entities.ROM.LOGIN.Company;
using RombiBack.Entities.ROM.LOGIN.Country;
using System;
using System.Collections.Generic;
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
        public Task<Country> Add(Country entity)
        {
            throw new NotImplementedException();
        }

        public Task<Country> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Country>> GetAll()
        {
            List<Country> countries = new List<Country>();

            try
            {
                using (SqlConnection sql = new SqlConnection(_dbConnection.GetConnectionAPP_BI()))
                {
                    await sql.OpenAsync();

                    string query = "select * from SEG_PAIS";

                    using (SqlCommand cmd = new SqlCommand(query, sql))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Country country = new Country
                                {
                                    COD_PAIS = reader["COD_PAIS"] != DBNull.Value ? reader["COD_PAIS"].ToString() : null,
                                    DESCRIPCION = reader["DESCRIPCION"] != DBNull.Value ? reader["DESCRIPCION"].ToString() : null,
                                    //VISIBLE = int.TryParse(reader["VISIBLE"].ToString(), out int visible) ? visible : (int?)null,
                                    //VISIBLE = Convert.ToInt32(reader["VISIBLE"] != DBNull.Value ? reader["VISIBLE"].ToString() : null),
                                    //VISIBLE = reader["VISIBLE"] != DBNull.Value && (bool)reader["VISIBLE"],
                                    VISIBLE = reader["VISIBLE"] != DBNull.Value ? Convert.ToInt32(reader["VISIBLE"]) : (int?)null,
                                    //ESTADO = reader["ESTADO"] != DBNull.Value && (bool)reader["ESTADO"],
                                    ESTADO = reader["ESTADO"] != DBNull.Value ? Convert.ToInt32(reader["ESTADO"]) : (int?)null,

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


        public Task<bool> Remove(Country entity)
        {
            throw new NotImplementedException();
        }

        public Task<Country> Update(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}
