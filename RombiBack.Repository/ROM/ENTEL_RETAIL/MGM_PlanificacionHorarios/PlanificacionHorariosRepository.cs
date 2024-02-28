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
                                    trn.horarioentrada = reader.GetTimeSpan(reader.GetOrdinal("horarioentrada"));
                                    trn.horariosalida = reader.GetTimeSpan(reader.GetOrdinal("horariosalida"));
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

        public async Task<string> PostTurnosSupervisor(TurnosSupervisorRequest turnossuper)
        {
            string mensaje = "";

            using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
            {
                await connection.OpenAsync();

                try
                {
                    SqlCommand cmd = new SqlCommand("USP_POSTTURNOSUPERVISOR", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@usuario", SqlDbType.VarChar) { Value = turnossuper.usuario });
                    cmd.Parameters.Add(new SqlParameter("@horarioentrada", SqlDbType.Time) { Value = turnossuper.horarioentrada });
                    cmd.Parameters.Add(new SqlParameter("@horariosalida", SqlDbType.Time) { Value = turnossuper.horariosalida });
                    cmd.Parameters.Add(new SqlParameter("@descripcion", SqlDbType.VarChar) { Value = turnossuper.descripcion });
                    cmd.Parameters.Add(new SqlParameter("@idtipoturno", SqlDbType.Int) { Value = turnossuper.idtipoturno });
                    var outputParameter = new SqlParameter("@mensaje", SqlDbType.VarChar, 100);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);
                    await cmd.ExecuteNonQueryAsync();
                    mensaje = cmd.Parameters["@mensaje"].Value.ToString();

                }
                catch (SqlException ex) when (ex.Number == 50000)
                {
                    // Manejar la excepción específica de horarios duplicados
                    throw new Exception("Ya existe un turno con el mismo horario para este usuario.", ex);
                }

            }
            return mensaje;

        }
    }
}
