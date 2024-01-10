using EccoBack.Abstraction;
using EccoBack.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EccoBack.Repository
{
    public class Repository<T> : IRepository<T>
    {
        private readonly DataAcces _dbConnection;
        public Repository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public IList<T> GetProducto()
        {
            IList<Producto> productos = new List<Producto>();

                using (IDbConnection db = _dbConnection.GetConnection())
                {
                    string query = "SELECT * FROM RETAIL_ModeloEquipo";

                    db.Open();

                    using (IDbCommand command = db.CreateCommand())
                    {
                        command.CommandText = query;

                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto
                                {
                                    intModeloEquipoID = reader["intModeloEquipoID"] != DBNull.Value ? (int)reader["intModeloEquipoID"] : (int?)null,
                                    strModeloEquipoDesc = reader["strModeloEquipoDesc"] != DBNull.Value ? reader["strModeloEquipoDesc"].ToString() : null,
                                    strModeloEquipoEstado = reader["strModeloEquipoEstado"].ToString(),
                                    strModeloEquipoUsuCre = reader["strModeloEquipoUsuCre"].ToString(),
                                    dteModeloEquipoFeCre = reader["dteModeloEquipoFeCre"].ToString(),
                                    strModeloEquipoUsuModi = reader["strModeloEquipoUsuModi"].ToString(),
                                    dteModeloEquipoFeModi = reader["dteModeloEquipoFeModi"].ToString(),
                                    strModeloEquipoUsuAnul = reader["strModeloEquipoUsuAnul"].ToString(),
                                    dteModeloEquipoFeAnul = reader["dteModeloEquipoFeAnul"].ToString()
                                };

                                productos.Add(producto);
                            }
                        }
                    }
                }

                return (IList<T>) productos;
            }
    }
}