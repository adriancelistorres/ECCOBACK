﻿using EccoBack.Abstraction;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Producto;
using EccoBack.Entities.ROM.ENTEL_RETAIL.Models.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EccoBack.Repository.ROM.ENTEL_RETAIL.MGM_Reports
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly DataAcces _dbConnection;
        public ReportsRepository(DataAcces dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public Task<Reports> Add(Reports entity)
        {
            throw new NotImplementedException();
        }

        public Task<Reports> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Reports>> GetAll()
        {
            using (SqlConnection connection = (SqlConnection)_dbConnection.GetConnection())
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT A.*, B.NOMBRE, B.ACTIVO " +
                                                          "FROM APP_BI.DBO.SEG_USUARIO_REPORTE A " +
                                                          "LEFT JOIN APP_BI.DBO.GOB_REPORTES_BI B ON A.IDREPORTE = B.CODIGO", connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<Reports> result = new List<Reports>();

                        while (await reader.ReadAsync())
                        {
                            Reports report = new Reports
                            {
                                USUARIO = reader["USUARIO"] is DBNull ? null : reader["USUARIO"].ToString(),
                                IDREPORTE = reader["IDREPORTE"] is DBNull ? (int?)null : Convert.ToInt32(reader["IDREPORTE"]),
                                NOMBRE = reader["NOMBRE"] is DBNull ? null : reader["NOMBRE"].ToString(),
                                ACTIVO = reader["ACTIVO"] is DBNull ? (int?)null : Convert.ToInt32(reader["ACTIVO"])
                            };

                            result.Add(report);
                        }

                        return result;
                    }
                }
            }

        }



        public Task<bool> Remove(Reports entity)
        {
            throw new NotImplementedException();
        }

        public Task<Reports> Update(Reports entity)
        {
            throw new NotImplementedException();
        }
    }
}
