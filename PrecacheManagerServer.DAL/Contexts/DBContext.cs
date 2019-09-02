using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PrecacheManagerServer.DAL.Contexts
{

    /// <summary>
    /// This class wil return a dbcontext - containing details of a dynamically  built sqlconnection
    /// </summary>
    public class DBContext
    {
        //the connection string to the database
        private string _connectionString;

        SqlConnection _sqlConnection;
        

        public DBContext(string connectionString)
        {
            _connectionString = connectionString;

            _sqlConnection = new SqlConnection(_connectionString);
        }

        public SqlConnection SqlConnection
        {
            get
            {
                return _sqlConnection;
            }
        }

    }
}
