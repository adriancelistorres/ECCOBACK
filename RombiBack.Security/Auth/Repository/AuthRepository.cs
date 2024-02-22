using AutoMapper;
using RombiBack.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiRestNetCore.DTO.DtoIncentivo;
using RombiBack.Security.Model.UserAuth;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RombiBack.Security.Auth.Repsitory
{
    public class AuthRepository:IAuthRepository
    {

        private readonly DataAcces _dbConnection;

        public AuthRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<UserDTOResponse> RombiLoginMain(UserDTORequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionAPP_BI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_RombiLoginMain", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@codempresa", SqlDbType.Char, 4).Value = request.codempresa; // Ajustar el tamaño del parámetro
                        command.Parameters.Add("@codpais", SqlDbType.Char, 4).Value = request.codpais; // Ajustar el tamaño del parámetro
                        command.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = request.user;
                        command.Parameters.Add("@password", SqlDbType.VarChar, 50).Value = request.password;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Acceso concedido
                                UserDTOResponse userAuth = new UserDTOResponse
                                {
                                    Resultado = reader["Resultado"].ToString(),
                                    Accede = Convert.ToInt32(reader["Accede"])
                                };
                                return userAuth;
                            }
                            else
                            {
                                // Acceso denegado
                                return new UserDTOResponse
                                {
                                    Resultado = reader["Resultado"].ToString(),
                                    Accede = Convert.ToInt32(reader["Accede"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error en ValidateUser: " + ex.Message);
                throw; // Lanzar excepción para que la capa superior maneje el error
            }
        }


        //public async Task<UserAuth> ValidateUser(UserDTORequest request)
        //{
        //    using SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionAPP_BI());
        //    using SqlCommand command = new SqlCommand("USP_ValidateUserRombi", connection);
        //    command.CommandType = CommandType.StoredProcedure;

        //    command.Parameters.AddWithValue("@CodPais", request.CodPais);
        //    command.Parameters.AddWithValue("@Usuario", request.Usuario);
        //    command.Parameters.AddWithValue("@Clave", request.Clave);

        //    await connection.OpenAsync();

        //    using SqlDataReader reader = await command.ExecuteReaderAsync();
        //    if (reader.Read())
        //    {
        //        // Mapea los resultados del procedimiento almacenado a un objeto Usuario
        //        UserAuth usuarios = new UserAuth
        //        {
        //            IdUsuario = reader.GetInt32(reader.GetOrdinal("IDUSUARIO")),
        //            Nombres = reader.GetString(reader.GetOrdinal("NOMBRES")),
        //            ApellidoPaterno = reader.GetString(reader.GetOrdinal("APELLIDOPATERNO")),
        //            ApellidoMaterno = reader.GetString(reader.GetOrdinal("APELLIDOMATERNO")),
        //            Correo = reader.GetString(reader.GetOrdinal("CORREO")),
        //            Usuario = reader.GetString(reader.GetOrdinal("USUARIO")),
        //            Clave = reader.GetString(reader.GetOrdinal("CLAVE")),
        //            CodPais = reader.GetString(reader.GetOrdinal("COD_PAIS")),
        //            CodNegocio = reader.GetString(reader.GetOrdinal("COD_NEGOCIO")),
        //            CodCuenta = reader.GetString(reader.GetOrdinal("COD_CUENTA")),
        //            EsAdmin = reader.GetBoolean(reader.GetOrdinal("ES_ADMIN"))
        //        };

        //        return usuarios;
        //    }

        //    return null; // Usuario no encontrado
        //}
        //public async Task<UserAuth> ValidateUser(UserDTORequest request)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionAPP_BI()))
        //        {
        //            await connection.OpenAsync();

        //            using (SqlCommand command = new SqlCommand("USP_ValidateUserRombi", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddWithValue("@CodPais", request.CodPais);
        //                command.Parameters.AddWithValue("@Usuario", request.Usuario);
        //                command.Parameters.AddWithValue("@Clave", request.Clave);

        //                using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        return new UserAuth
        //                        {
        //                            IdUsuario = reader.GetInt32(reader.GetOrdinal("IDUSUARIO")),
        //                            Nombres = reader.GetString(reader.GetOrdinal("NOMBRES")),
        //                            ApellidoPaterno = reader.GetString(reader.GetOrdinal("APELLIDOPATERNO")),
        //                            ApellidoMaterno = reader.GetString(reader.GetOrdinal("APELLIDOMATERNO")),
        //                            Correo = reader.GetString(reader.GetOrdinal("CORREO")),
        //                            Usuario = reader.GetString(reader.GetOrdinal("USUARIO")),
        //                            Clave = reader.GetString(reader.GetOrdinal("CLAVE")),
        //                            CodPais = reader.GetString(reader.GetOrdinal("COD_PAIS")),
        //                            CodNegocio = reader.GetString(reader.GetOrdinal("COD_NEGOCIO")),
        //                            CodCuenta = reader.GetString(reader.GetOrdinal("COD_CUENTA")),
        //                            EsAdmin = reader.GetBoolean(reader.GetOrdinal("ES_ADMIN"))
        //                        };
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción de manera adecuada
        //        // Registrar o lanzar la excepción según corresponda
        //        Console.WriteLine($"Error en ValidateUser: {ex.Message}");
        //    }

        //    return null; // Usuario no encontrado
        //}
        public async Task<UserAuth> ValidateUser(UserDTORequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionAPP_BI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_ValidateUserRombi", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@CodPais", SqlDbType.VarChar, 50).Value = request.codpais;
                        command.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = request.user;
                        command.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = request.password;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                await reader.ReadAsync();
                                return GetUserFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción de manera adecuada
                // Registrar o lanzar la excepción según corresponda
                Console.WriteLine($"Error en ValidateUser: {ex.Message}");
            }

            return null; // Usuario no encontrado
        }

        private UserAuth GetUserFromReader(SqlDataReader reader)
        {
            return new UserAuth
            {
                idusuario = reader.GetInt32(reader.GetOrdinal("IDUSUARIO")),
                nombres = reader.GetString(reader.GetOrdinal("NOMBRES")),
                apellidopaterno = reader.GetString(reader.GetOrdinal("APELLIDOPATERNO")),
                apellidomaterno = reader.GetString(reader.GetOrdinal("APELLIDOMATERNO")),
                idjerarquia = reader.GetInt32(reader.GetOrdinal("IDJERARQUIA")),
                correo = reader.GetString(reader.GetOrdinal("CORREO")),
                usuario = reader.GetString(reader.GetOrdinal("USUARIO")),
                clave = reader.GetString(reader.GetOrdinal("CLAVE")),
                cod_pais = reader.GetString(reader.GetOrdinal("COD_PAIS")),
                cod_negocio = reader.GetString(reader.GetOrdinal("COD_NEGOCIO")),
                cod_cuenta = reader.GetString(reader.GetOrdinal("COD_CUENTA")),
                es_admin = reader.GetString(reader.GetOrdinal("ES_ADMIN"))
            };
        }




    }
}
