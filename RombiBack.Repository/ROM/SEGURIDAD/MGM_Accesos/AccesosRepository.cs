using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using RombiBack.Entities.ROM.SEGURIDAD.Models.Accesos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RombiBack.Repository.ROM.SEGURIDAD.MGM_Accesos
{
    public class AccesosRepository:IAccesosRepository
    {
        private readonly DataAcces _dbConnection;

        public AccesosRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<Accesos>> GetAccesos()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETACCESOS", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<Accesos> response = new List<Accesos>();

                                while (await reader.ReadAsync())
                                {
                                    Accesos accs = new Accesos();
                                    accs.idacceso = reader.GetInt32(reader.GetOrdinal("idacceso"));
                                    accs.dni = reader.GetString(reader.GetOrdinal("dni"));
                                    accs.perfil = reader.GetString(reader.GetOrdinal("perfil"));
                                    accs.nombrecompleto = reader.GetString(reader.GetOrdinal("nombrecompleto"));
                                   

                                    response.Add(accs);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<Accesos>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
