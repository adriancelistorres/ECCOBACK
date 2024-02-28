using Microsoft.EntityFrameworkCore;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Abstraction;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.PortableExecutable;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_PlanificacionHorarios
{
    public class PlanificacionHorariosRepository : IPlanificacionHorariosRepository
    {
        private readonly DataAcces _dbConnection;

        public PlanificacionHorariosRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<List<TurnosSupervisor>> GetTurnosSupervisor(string usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTURNOSSUPERVISOR", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                  

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<TurnosSupervisor> response = new List<TurnosSupervisor>();

                                while (await reader.ReadAsync())
                                {
                                    TurnosSupervisor trn = new TurnosSupervisor();
                                    trn.idturnos = reader.GetInt32(reader.GetOrdinal("idturnos"));
                                    trn.usuario = reader.GetString(reader.GetOrdinal("usuario"));
                                    trn.horarioentrada = reader.GetString(reader.GetOrdinal("horarioentrada"));
                                    trn.horariosalida = reader.GetString(reader.GetOrdinal("horariosalida"));
                                    trn.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                    trn.idtipoturno = reader.GetInt32(reader.GetOrdinal("idtipoturno"));
                                    trn.estado = reader.GetInt32(reader.GetOrdinal("estado"));
                                    trn.fecha_creacion = reader.GetDateTime(reader.GetOrdinal("fecha_creacion"));
                                    trn.fecha_modificacion = reader.GetDateTime(reader.GetOrdinal("fecha_modificacion"));
                                    trn.usuario_creacion = reader.GetString(reader.GetOrdinal("usuario_creacion"));
                                    trn.usuario_modificacion = reader.GetString(reader.GetOrdinal("usuario_modificacion"));

                                    response.Add(trn);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<TurnosSupervisor>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // Por ejemplo, podrías registrar el error y devolver un mensaje de error adecuado
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }
        }

        public async Task<Respuesta> PostTurnosSupervisor(TurnosSupervisorRequest turnosSupervisor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_POSTTURNOSUPERVISOR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnosSupervisor.usuario;
                        cmd.Parameters.Add("@horarioentrada", SqlDbType.VarChar).Value = turnosSupervisor.horarioentrada;
                        cmd.Parameters.Add("@horariosalida", SqlDbType.VarChar).Value = turnosSupervisor.horariosalida;
                        cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = turnosSupervisor.descripcion;
                        cmd.Parameters.Add("@idtipoturno", SqlDbType.Int).Value = turnosSupervisor.idtipoturno;

                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {
                            //if (rdr.Read())
                            //{
                            //    Respuesta rt = new Respuesta();
                            //    rdr = rt.Mensaje;

                            //}

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
    }



}

