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


        public async Task<Respuesta> PutTurnosSupervisor(TurnosSupervisor turnossuper)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_PUTTURNOSSUPERVISOR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure; 
                        cmd.Parameters.Add("@idturnos", SqlDbType.Int).Value = turnossuper.idturnos;
                        cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnossuper.usuario;
                        cmd.Parameters.Add("@horarioentrada", SqlDbType.VarChar).Value = turnossuper.horarioentrada;
                        cmd.Parameters.Add("@horariosalida", SqlDbType.VarChar).Value = turnossuper.horariosalida;
                        cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = turnossuper.descripcion;

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

        public async Task<Respuesta> DeleteTurnosSupervisor(TurnosSupervisor turnossuper)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_DELETETURNOSSUPERVISOR", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idturnos", SqlDbType.Int).Value = turnossuper.idturnos;
                        cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnossuper.usuario;
                       

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



        public async Task<List<SupervisorPdvResponse>> GetSupervisorPDV(string usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETSUPERVISORPDV", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<SupervisorPdvResponse> response = new List<SupervisorPdvResponse>();

                                while (await reader.ReadAsync())
                                {
                                    SupervisorPdvResponse supervisorpdv = new SupervisorPdvResponse();
                                    supervisorpdv.usuario = reader.GetString(reader.GetOrdinal("usuario"));
                                    supervisorpdv.idpuntoventarol = reader.GetInt32(reader.GetOrdinal("idpuntoventa_rol"));
                                    supervisorpdv.puntoventa = reader.GetString(reader.GetOrdinal("puntoventa"));
                                   

                                    response.Add(supervisorpdv);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<SupervisorPdvResponse>(); // Devuelve una lista vacía en lugar de null
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



        public async Task<List<TurnosSupervisor>> GetTurnosDisponiblePDV(TurnosDisponiblesPdvRequest turnodispo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTURNOSDISPONIBLESPDV", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnodispo.usuario;
                        command.Parameters.Add("@idpdv", SqlDbType.Int).Value = turnodispo.idpdv;


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<TurnosSupervisor> response = new List<TurnosSupervisor>();

                                while (await reader.ReadAsync())
                                {
                                    TurnosSupervisor supervisorpdv = new TurnosSupervisor();
                                    supervisorpdv.idturnos = reader.GetInt32(reader.GetOrdinal("idturnos"));
                                    supervisorpdv.usuario = reader.GetString(reader.GetOrdinal("usuario"));
                                    supervisorpdv.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                    supervisorpdv.horarioentrada = reader.GetString(reader.GetOrdinal("horarioentrada"));
                                    supervisorpdv.horariosalida = reader.GetString(reader.GetOrdinal("horariosalida"));
                                    supervisorpdv.estado = reader.GetInt32(reader.GetOrdinal("estado"));


                                    response.Add(supervisorpdv);
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

        public async Task<Respuesta> PostTurnosPDV(List<TurnosPdvRequest> turnosPdvList)
        {
            try
            {
                Respuesta ultimaRespuesta = new Respuesta(); // Inicializamos la respuesta fuera del bucle

                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    foreach (var turnospdv in turnosPdvList)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_POSTTURNOSPDV", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnospdv.usuario;
                            cmd.Parameters.Add("@idpdv", SqlDbType.Int).Value = turnospdv.idpdv;
                            cmd.Parameters.Add("@puntoventa", SqlDbType.VarChar).Value = turnospdv.puntoventa;
                            cmd.Parameters.Add("@idturnos", SqlDbType.Int).Value = turnospdv.idturnos;

                            using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                            {
                                while (await rdr.ReadAsync())
                                {
                                    ultimaRespuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));
                                }
                            }
                        }
                    }
                }
                // Devolver la última respuesta obtenida
                return ultimaRespuesta;
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



        public async Task<List<TurnosSupervisor>> GetTurnosAsignadosPDV(TurnosDisponiblesPdvRequest turnodispo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETTURNOSASIGNADOSPDV", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@usuario", SqlDbType.VarChar).Value = turnodispo.usuario;
                        command.Parameters.Add("@idpdv", SqlDbType.Int).Value = turnodispo.idpdv;


                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<TurnosSupervisor> response = new List<TurnosSupervisor>();

                                while (await reader.ReadAsync())
                                {
                                    TurnosSupervisor supervisorpdv = new TurnosSupervisor();
                                    supervisorpdv.idpdvturno = reader.GetInt32(reader.GetOrdinal("idpdvturno"));
                                    supervisorpdv.idturnos = reader.GetInt32(reader.GetOrdinal("idturnos"));
                                    supervisorpdv.usuario = reader.GetString(reader.GetOrdinal("usuario"));
                                    supervisorpdv.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                                    supervisorpdv.horarioentrada = reader.GetString(reader.GetOrdinal("horarioentrada"));
                                    supervisorpdv.horariosalida = reader.GetString(reader.GetOrdinal("horariosalida"));
                                    //supervisorpdv.estado = reader.GetInt32(reader.GetOrdinal("estado"));


                                    response.Add(supervisorpdv);
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

        public async Task<Respuesta> DeleteTurnosPDV(TurnosPdvRequest turnospdv)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_DELETETURNOSPDV", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idpdvturno", SqlDbType.Int).Value = turnospdv.idpdvturno;
                       
                        using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                        {

                            Respuesta respuesta = new Respuesta();
                            while (await rdr.ReadAsync())
                            {
                                respuesta.Mensaje = rdr.GetString(rdr.GetOrdinal("Mensaje"));

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

