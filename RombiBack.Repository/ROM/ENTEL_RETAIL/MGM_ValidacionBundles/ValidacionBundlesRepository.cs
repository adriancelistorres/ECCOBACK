using RombiBack.Abstraction;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RombiBack.Entities.ROM.ENTEL_RETAIL.Models.ValidacionBundles.ValidacionBundle;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.PlanificacionHorarios;

namespace RombiBack.Repository.ROM.ENTEL_RETAIL.MGM_ValidacionBundles
{
    public class ValidacionBundlesRepository : IValidacionBundlesRepository
    {
        private readonly DataAcces _dbConnection;

        public ValidacionBundlesRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<ValidacionBundle> GetBundlesVentas(int intIdVentasPrincipal)
        {
            
            try
            {
                using (SqlConnection connection = new SqlConnection(_dbConnection.GetConnectionROMBI()))
                {
                    await connection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand("USP_GETBUNDLESVENTAS", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@intIdVentasPrincipal", SqlDbType.Int).Value = intIdVentasPrincipal;

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            ValidacionBundle respuesta =  new ValidacionBundle();
                            while (await reader.ReadAsync())
                            {

                                respuesta.intidventasprincipal = reader.GetInt32(reader.GetOrdinal("intidventasprincipal"));
                                respuesta.intventasromid = reader.GetInt32(reader.GetOrdinal("intventasromid"));
                                respuesta.strdtevestasromfeope = reader.GetString(reader.GetOrdinal("dtevestasromfeope"));
                                respuesta.strdnicliente = reader.GetString(reader.GetOrdinal("strdnicliente"));
                                respuesta.strventasromusucr = reader.GetString(reader.GetOrdinal("dnipromotor"));
                                respuesta.nombrepromotor = reader.GetString(reader.GetOrdinal("nombrepromotor"));
                                respuesta.intproductoid = reader.GetInt32(reader.GetOrdinal("intproductoid"));
                                respuesta.strproductodesc = reader.GetString(reader.GetOrdinal("strproductodesc"));
                                respuesta.intplanid = reader.GetInt32(reader.GetOrdinal("intplanid"));
                                respuesta.strplandesc = reader.GetString(reader.GetOrdinal("strplandesc"));
                                respuesta.intmodeloequipoid = reader.GetInt32(reader.GetOrdinal("intmodeloequipoid"));
                                respuesta.strmodeloequipodesc = reader.GetString(reader.GetOrdinal("strmodeloequipodesc"));
                                respuesta.intbundleid = reader.GetInt32(reader.GetOrdinal("intbundleid"));
                                respuesta.descripcion = reader.GetString(reader.GetOrdinal("descripcion"));
                               
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
                    throw new InvalidOperationException("Ocurrió un error al obtener las ventas.");
                }
            }
        }

    }
}
