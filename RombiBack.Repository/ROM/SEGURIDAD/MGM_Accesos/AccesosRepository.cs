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

 

        public async Task<Respuesta> PostAccesos(AccesosRequest accs)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_POSTACCESOS", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idperfil", SqlDbType.Int).Value = accs.idperfil;
                        cmd.Parameters.Add("@dni", SqlDbType.VarChar).Value = accs.dni;
                        cmd.Parameters.Add("@usuariocreacion", SqlDbType.VarChar).Value = accs.usuariocreacion;
                      
                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            Respuesta respuesta = new Respuesta();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));

                                // Puedes manejar múltiples filas si es necesario
                                // Por ejemplo, almacenar cada resultado en una lista
                            }

                            return respuesta;
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }

        public async Task<Accesos> GetSegUsuario(string usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_GETSEGUSUARIO", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            Accesos respuesta = new Accesos();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.usuario = rdr.GetString(rdr.GetOrdinal("usuario"));
                                respuesta.nombrecompleto = rdr.GetString(rdr.GetOrdinal("nombrecompleto"));

                            }

                            return respuesta;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    // Código 2627 y 2601: Violación de restricción de clave única
                    throw new InvalidOperationException("Ya existe un turno con el mismo horario para este usuario.");
                }
                else
                {
                    // Otros errores de base de datos
                    throw new InvalidOperationException("Ocurrió un error al insertar el turno.");
                }
            }
        }
    }
}
