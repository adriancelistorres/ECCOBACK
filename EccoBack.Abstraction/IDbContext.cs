using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;



namespace EccoBack.Abstraction
{
    public class IDbContext: DbContext
    {
        private readonly string connectionString;

        //public IDbContext()
        //{
        //    connectionString = ConfigurationManager.ConnectionStrings["MBConnection"]?.ConnectionString;
        //}

        //public IDbConnection GetConnection()
        //{
        //    return new SqlConnection(connectionString);
        //}


    }
}