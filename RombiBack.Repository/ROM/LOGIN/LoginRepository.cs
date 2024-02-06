using RombiBack.Entities.ROM.LOGIN;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RombiBack.Abstraction;

namespace RombiBack.Repository.ROM.LOGIN
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DataAcces _dbConnection;

        public LoginRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public SEG_UsuarioBE ValidateUser(SEG_UsuarioBE usuario)
        {
            SEG_UsuarioBE usuarioRetorno = new SEG_UsuarioBE();

            // Obteniendo la cadena de conexión desde _dbConnection
            string connectionString = _dbConnection.GetConnectionAPP_BI();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SEG_ValidateUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    cn.Open();
                    cmd.Parameters.Add("@Usuario", SqlDbType.VarChar, 50).Value = usuario.usuario;
                    cmd.Parameters.Add("@Clave", SqlDbType.VarChar, 50).Value = usuario.clave;
                    cmd.Parameters.Add("@CodNegocio", SqlDbType.Char, 3).Value = usuario.COD_NEGOCIO;
                    cmd.Parameters.Add("@CodCuenta", SqlDbType.Char, 3).Value = usuario.COD_CUENTA;
                    cmd.Parameters.Add("@CodPais", SqlDbType.Char, 4).Value = usuario.cod_pais;

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            usuarioRetorno.IDUSUARIO = int.Parse(rdr["IDUSUARIO"].ToString());
                            usuarioRetorno.NOMBRES = rdr["NOMBRES"].ToString();
                            usuarioRetorno.APELLIDOPATERNO = rdr["APELLIDOPATERNO"].ToString();
                            usuarioRetorno.APELLIDOMATERNO = rdr["APELLIDOMATERNO"].ToString();
                            usuarioRetorno.JERARQUIA = rdr["JERARQUIA"].ToString();
                            usuarioRetorno.IDJERARQUIA = int.Parse(rdr["IDJERARQUIA"].ToString());
                            usuarioRetorno.CORREO = (rdr["CORREO"] != null ? rdr["CORREO"].ToString() : "");
                            usuarioRetorno.usuario = rdr["USUARIO"].ToString();
                            usuarioRetorno.COD_NEGOCIO = rdr["COD_NEGOCIO"].ToString();
                            usuarioRetorno.COD_CUENTA = rdr["COD_CUENTA"].ToString();
                            usuarioRetorno.cod_pais = rdr["COD_PAIS"].ToString();
                            usuarioRetorno.clave = rdr["CLAVE"].ToString();
                            usuarioRetorno.ES_ADMIN = rdr["ES_ADMIN"].ToString();
                            usuarioRetorno.TOKEN = rdr["TOKEN"].ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) { cn.Close(); }
                    cmd.Dispose();
                }
            }
            return usuarioRetorno;
        }

    }
}
