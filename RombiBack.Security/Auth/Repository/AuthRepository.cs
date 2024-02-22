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
using System.Reflection;
using RombiBack.Security.Model.UserAuth.Modules;

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
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_ROMBILOGIN", connection))
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
        public async Task<List<BusinessAccountResponse>> GetBusinessUser(UserDTORequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETNEGOCIOUSER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EMPRESAID", SqlDbType.Char, 4).Value = request.codempresa;
                        command.Parameters.Add("@USUARIO", SqlDbType.Char, 50).Value = request.user;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<BusinessAccountResponse> response = new List<BusinessAccountResponse>();

                                while (await reader.ReadAsync())
                                {
                                    BusinessAccountResponse businessAccount = new BusinessAccountResponse();
                                    businessAccount.idpais = reader.GetString(reader.GetOrdinal("idpais"));
                                    businessAccount.idnegocio = reader.GetString(reader.GetOrdinal("idnegocio"));
                                    businessAccount.desc_negocio = reader.GetString(reader.GetOrdinal("desc_negocio"));

                                    response.Add(businessAccount);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<BusinessAccountResponse>(); // Devuelve una lista vacía en lugar de null
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }
        }

        public async Task<List<BusinessAccountResponse>> GetBusinessAccountUser(UserDTORequest request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETCUENTAUSER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@EMPRESAID", SqlDbType.Char, 4).Value = request.codempresa;
                        command.Parameters.Add("@COD_NEGOCIO", SqlDbType.Char, 50).Value = request.codnegocio;
                        command.Parameters.Add("@USUARIO", SqlDbType.Char, 50).Value = request.user;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                List<BusinessAccountResponse> response = new List<BusinessAccountResponse>();

                                while (await reader.ReadAsync())
                                {
                                    BusinessAccountResponse businessAccount = new BusinessAccountResponse();
                                    businessAccount.idpais = reader.GetString(reader.GetOrdinal("idpais"));
                                    businessAccount.idnegocio = reader.GetString(reader.GetOrdinal("idnegocio"));
                                    businessAccount.desc_negocio = reader.GetString(reader.GetOrdinal("desc_negocio"));
                                    businessAccount.idcuenta = reader.GetString(reader.GetOrdinal("idcuenta"));
                                    businessAccount.desc_cuenta = reader.GetString(reader.GetOrdinal("desc_cuenta"));

                                    response.Add(businessAccount);
                                }

                                return response;
                            }
                            else
                            {
                                // No se encontraron resultados
                                return new List<BusinessAccountResponse>(); // Devuelve una lista vacía en lugar de null
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

        public async Task<List<ModuloDTOResponse>> GetPermissions(UserDTORequest request)
        {
            List<ModuloDTOResponse> permissions = new List<ModuloDTOResponse>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("USP_GETPERMITSUSER", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@IDPAIS", SqlDbType.VarChar, 50).Value = request.codpais;
                        command.Parameters.Add("@IDEMPRESA", SqlDbType.VarChar, 50).Value = request.codempresa;
                        command.Parameters.Add("@IDNEGOCIO", SqlDbType.VarChar, 50).Value = request.codnegocio;
                        command.Parameters.Add("@IDCUENTA", SqlDbType.VarChar, 50).Value = request.codcuenta;
                        command.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = request.user;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            Dictionary<int, ModuloDTOResponse> moduloDictionary = new Dictionary<int, ModuloDTOResponse>();

                            while (await reader.ReadAsync())
                            {
                                int idmodulo = reader.IsDBNull(reader.GetOrdinal("idmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("idmodulo"));

                                if (!moduloDictionary.ContainsKey(idmodulo))
                                {
                                    ModuloDTOResponse module = new ModuloDTOResponse
                                    {
                                        idmodulo = idmodulo,
                                        nombremodulo = reader.IsDBNull(reader.GetOrdinal("nombremodulo")) ? null : reader.GetString(reader.GetOrdinal("nombremodulo")),
                                        iconomodulo = reader.IsDBNull(reader.GetOrdinal("iconomodulo")) ? null : reader.GetString(reader.GetOrdinal("iconomodulo")),
                                        rutamodulo = reader.IsDBNull(reader.GetOrdinal("rutamodulo")) ? null : reader.GetString(reader.GetOrdinal("rutamodulo")),
                                        nivelmodulo = reader.IsDBNull(reader.GetOrdinal("nivelmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("nivelmodulo")),
                                        ordenmodulo = reader.IsDBNull(reader.GetOrdinal("ordenmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("ordenmodulo")),
                                        estadomodulo = reader.IsDBNull(reader.GetOrdinal("estadomodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("estadomodulo")),
                                        submodules = new List<SubModuloDTOResponse>()
                                    };

                                    moduloDictionary.Add(idmodulo, module);
                                }

                                SubModuloDTOResponse submodule = new SubModuloDTOResponse
                                {
                                    idsubmodulo = reader.IsDBNull(reader.GetOrdinal("idsubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("idsubmodulo")),
                                    nombresubmodulo = reader.IsDBNull(reader.GetOrdinal("nombresubmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombresubmodulo")),
                                    iconosubmodulo = reader.IsDBNull(reader.GetOrdinal("iconosubmodulo")) ? null : reader.GetString(reader.GetOrdinal("iconosubmodulo")),
                                    rutasubmodulo = reader.IsDBNull(reader.GetOrdinal("rutasubmodulo")) ? null : reader.GetString(reader.GetOrdinal("rutasubmodulo")),
                                    nivelsubmodulo = reader.IsDBNull(reader.GetOrdinal("nivelsubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("nivelsubmodulo")),
                                    ordensubmodulo = reader.IsDBNull(reader.GetOrdinal("ordensubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("ordensubmodulo")),
                                    estadosubmodulo = reader.IsDBNull(reader.GetOrdinal("estadosubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("estadosubmodulo")),
                                    items = new List<ItemModuloDTOResponse>()
                                };

                                ItemModuloDTOResponse item = new ItemModuloDTOResponse
                                {
                                    iditemmodulo = reader.IsDBNull(reader.GetOrdinal("iditemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("iditemmodulo")),
                                    nombreitemmodulo = reader.IsDBNull(reader.GetOrdinal("nombreitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombreitemmodulo")),
                                    iconoitemmodulo = reader.IsDBNull(reader.GetOrdinal("iconoitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("iconoitemmodulo")),
                                    rutaitemmodulo = reader.IsDBNull(reader.GetOrdinal("rutaitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("rutaitemmodulo")),
                                    nivelitemmodulo = reader.IsDBNull(reader.GetOrdinal("nivelitemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("nivelitemmodulo")),
                                    ordenitemmodulo = reader.IsDBNull(reader.GetOrdinal("ordenitemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("ordenitemmodulo")),
                                    estadoitemmodulo = reader.IsDBNull(reader.GetOrdinal("estadoitemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("estadoitemmodulo"))
                                };

                                ModuloDTOResponse currentModule = moduloDictionary[idmodulo];
                                var existingSubmodule = currentModule.submodules.FirstOrDefault(s => s.idsubmodulo == submodule.idsubmodulo);
                                if (existingSubmodule != null)
                                {
                                    existingSubmodule.items.Add(item);
                                }
                                else
                                {
                                    currentModule.submodules.Add(submodule);
                                    submodule.items.Add(item);
                                }
                            }

                            permissions.AddRange(moduloDictionary.Values);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine("Error: " + ex.Message);
                throw; // O devuelve algún tipo de indicación de error adecuada
            }

            return permissions;
        }



        //public async Task<List<ModuloDTOResponse>> GetPermissions(UserDTORequest request)
        //{
        //    List<ModuloDTOResponse> permissions = new List<ModuloDTOResponse>();

        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
        //        {
        //            await connection.OpenAsync();

        //            using (SqlCommand command = new SqlCommand("USP_GETPERMITSUSER", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.Add("@IDPAIS", SqlDbType.VarChar, 50).Value = request.codpais;
        //                command.Parameters.Add("@IDEMPRESA", SqlDbType.VarChar, 50).Value = request.codempresa;
        //                command.Parameters.Add("@IDNEGOCIO", SqlDbType.VarChar, 50).Value = request.codnegocio;
        //                command.Parameters.Add("@IDCUENTA", SqlDbType.VarChar, 50).Value = request.codcuenta;
        //                command.Parameters.Add("@USUARIO", SqlDbType.VarChar, 50).Value = request.user;

        //                using (SqlDataReader reader = await command.ExecuteReaderAsync())
        //                {
        //                    if (reader.HasRows)
        //                    {
        //                        while (await reader.ReadAsync())
        //                        {

        //                            ModuloDTOResponse module = new ModuloDTOResponse
        //                            {
        //                                idmodulo = reader.IsDBNull(reader.GetOrdinal("idmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("idmodulo")),
        //                                nombremodulo = reader.IsDBNull(reader.GetOrdinal("nombremodulo")) ? null : reader.GetString(reader.GetOrdinal("nombremodulo")),
        //                                iconomodulo = reader.IsDBNull(reader.GetOrdinal("iconomodulo")) ? null : reader.GetString(reader.GetOrdinal("iconomodulo")),
        //                                rutamodulo = reader.IsDBNull(reader.GetOrdinal("rutamodulo")) ? null : reader.GetString(reader.GetOrdinal("rutamodulo")),
        //                                nivelmodulo = reader.IsDBNull(reader.GetOrdinal("nivelmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("nivelmodulo")),
        //                                ordenmodulo = reader.IsDBNull(reader.GetOrdinal("ordenmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("ordenmodulo")),
        //                                estadomodulo = reader.IsDBNull(reader.GetOrdinal("estadomodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("estadomodulo")),
        //                                submodules = new List<SubModuloDTOResponse>()
        //                            };

        //                            SubModuloDTOResponse submodule = new SubModuloDTOResponse
        //                            {
        //                                idsubmodulo = reader.IsDBNull(reader.GetOrdinal("idsubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("idsubmodulo")),
        //                                nombresubmodulo = reader.IsDBNull(reader.GetOrdinal("nombresubmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombresubmodulo")),
        //                                iconosubmodulo = reader.IsDBNull(reader.GetOrdinal("iconosubmodulo")) ? null : reader.GetString(reader.GetOrdinal("iconosubmodulo")),
        //                                rutasubmodulo = reader.IsDBNull(reader.GetOrdinal("rutasubmodulo")) ? null : reader.GetString(reader.GetOrdinal("rutasubmodulo")),
        //                                nivelsubmodulo = reader.IsDBNull(reader.GetOrdinal("nivelsubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("nivelsubmodulo")),
        //                                ordensubmodulo = reader.IsDBNull(reader.GetOrdinal("ordensubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("ordensubmodulo")),
        //                                estadosubmodulo = reader.IsDBNull(reader.GetOrdinal("estadosubmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("estadosubmodulo")),
        //                                items = new List<ItemModuloDTOResponse>()
        //                            };

        //                            ItemModuloDTOResponse item = new ItemModuloDTOResponse
        //                            {
        //                                iditemmodulo = reader.IsDBNull(reader.GetOrdinal("iditemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("iditemmodulo")),
        //                                nombreitemmodulo = reader.IsDBNull(reader.GetOrdinal("nombreitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("nombreitemmodulo")),
        //                                iconoitemmodulo = reader.IsDBNull(reader.GetOrdinal("iconoitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("iconoitemmodulo")),
        //                                rutaitemmodulo = reader.IsDBNull(reader.GetOrdinal("rutaitemmodulo")) ? null : reader.GetString(reader.GetOrdinal("rutaitemmodulo")),
        //                                nivelitemmodulo = reader.IsDBNull(reader.GetOrdinal("nivelitemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("nivelitemmodulo")),
        //                                ordenitemmodulo = reader.IsDBNull(reader.GetOrdinal("ordenitemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("ordenitemmodulo")),
        //                                estadoitemmodulo = reader.IsDBNull(reader.GetOrdinal("estadoitemmodulo")) ? 0 : reader.GetInt32(reader.GetOrdinal("estadoitemmodulo"))
        //                            };
        //                            submodule.items.Add(item);
        //                            module.submodules.Add(submodule);
        //                            permissions.Add(module);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("Error: ");

        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción
        //        Console.WriteLine("Error: " + ex.Message);
        //        throw; // O devuelve algún tipo de indicación de error adecuada
        //    }

        //    return permissions;
        //}


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
